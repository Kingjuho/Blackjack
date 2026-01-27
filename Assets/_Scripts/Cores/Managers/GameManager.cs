using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager instance;

    // 딜러, 플레이어
    public Player Player;
    public Dealer Dealer;

    // 덱
    public Deck Deck { get; private set; }

    // 현재 상태
    private IGameState _currentState;

    [Header("Audio")]
    public AudioClip BGM;

    [Header("Resources")]
    public GameObject CardPrefab;
    public SpriteAtlas CardAtlas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // BGM 재생
        if (SoundManager.instance != null && BGM != null) SoundManager.instance.PlayBGM(BGM);

        // 덱 초기화
        Deck = new Deck();

        // 베팅 상태로 변경
        ChangeState(new BettingState());
    }

    // 상태 변경
    public void ChangeState(IGameState newState)
    {
        _currentState = newState;
        _currentState.Init(this);
    }

    // 버튼이 호출할 함수
    public void OnChipButton(int amount) => _currentState.OnBet(this, amount);
    public void OnDealButton() => _currentState.OnDeal(this);
    public void OnHitButton() => _currentState.OnHit(this);
    public void OnStandButton() => _currentState.OnStand(this);
    public void OnRetryButton() 
    {
        // 판 초기화
        Player.ClearHand();
        Dealer.ClearHand();

        ChangeState(new BettingState()); 
    }

    // 상태 클래스를 위한 헬퍼 함수
    public void UpdateMoneyUI()
    {
        UIManager.instance.UpdateMoneyUI(Player.OwnedMoney, Player.BetAmount);
    }

    // 초기화
    public void Initialize()
    {
        // 덱 초기화
        Deck.Initialize();
        Deck.Shuffle();

        // 초기 카드 분배
        Card dealerCard1 = Deck.DrawCard();
        Card dealerCard2 = Deck.DrawCard();

        dealerCard1.IsFaceUp = false;    // 한 장은 뒤집어줘야 함

        Dealer.AddCard(dealerCard1);
        Dealer.AddCard(dealerCard2);

        Player.AddCard(Deck.DrawCard());   // 뒤집을 필요 없으니 바로 삽입
        Player.AddCard(Deck.DrawCard());
    }

    // 딜러 자동 프로세스
    public IEnumerator DealerProcess()
    {
        // TODO: 이 함수 시작부터 CalculateResult() 함수의 끝부분까지 Hit, Stand 버튼을 누를 수 없어야 함

        // 딜러의 첫 번째 카드 공개
        Dealer.RevealFirstCard();

        // 1초 대기
        yield return new WaitForSeconds(1.0f);

        // 16점 이하일 시 딜러 드로우
        while (Dealer.CalculateScore() <= 16)
        {
            Dealer.AddCard(Deck.DrawCard());

            // 1초 대기
            yield return new WaitForSeconds(1.0f);
        }

        CalculateResult();
    }

    // 점수 계산
    void CalculateResult()
    {
        int playerScore = Player.CalculateScore();
        int dealerScore = Dealer.CalculateScore();

        // 블랙잭 확인
        bool playerBlackJack = Player.IsBlackJack;
        bool dealerBlackJack = Dealer.IsBlackJack;

        // 결과 정산
        // 딜러 버스트(플레이어 승)
        if (dealerScore > 21)
        {
            ChangeState(new ResultState("Dealer Bust! You Win!", 0, playerBlackJack));
        }
        // 플레이어 점수가 높음(플레이어 승)
        else if (playerScore > dealerScore)
        {
            ChangeState(new ResultState("You Win!", 0, playerBlackJack));
        }
        // 딜러 점수가 더 높음(플레이어 패)
        else if (playerScore < dealerScore)
        {
            ChangeState(new ResultState("Dealer Win...", 2));
        }
        // 무승부
        else
        {
            // 플레이어는 블랙잭, 딜러는 일반 21
            if (playerBlackJack && !dealerBlackJack) ChangeState(new ResultState("You Win!", 0, playerBlackJack));
            // 플레이어는 일반 21, 딜러는 블랙잭
            else if (!playerBlackJack && dealerBlackJack) ChangeState(new ResultState("Dealer Win...", 2));
            // 그 외
            else ChangeState(new ResultState("Push", 1));
        }
    }
}