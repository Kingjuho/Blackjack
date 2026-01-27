using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager instance;

    // 딜러, 플레이어
    public Player PlayerHand;
    public Dealer DealerHand;

    // 덱
    Deck _deck;

    [Header("Audio")]
    public AudioClip BGM;

    [Header("Resources")]
    public GameObject CardPrefab;
    public SpriteAtlas CardAtlas;

    [Header("UI References")]
    public GameObject ActionPanel;
    public GameObject CoinPanel;
    public GameObject ResultPanel;
    public TextMeshProUGUI ResultText;

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

    // 히트 버튼
    public void OnHitButton()
    {
        // 패에 카드 1장 추가
        PlayerHand.AddCard(_deck.DrawCard());

        // 점수 체크
        int score = PlayerHand.CalculateScore();
        Debug.Log($"Player Hit! Score: {score}");

        // 블랙잭/버스트 체크
        if (score > 21) ProcessResult($"Bust!\nYou Lose..");
        else if (score == 21) OnStandButton();
    }

    // 스탠드 버튼
    public void OnStandButton()
    {
        Debug.Log("Player Stand!");
        ActionPanel.SetActive(false);

        // 딜러 자동 프로세스 시작
        StartCoroutine(DealerProcess());
    }

    // 리트라이 버튼
    public void OnRetryButton()
    {
        Initialize();
    }

    // 딜러 자동 프로세스
    IEnumerator DealerProcess()
    {
        // 딜러의 첫 번째 카드 공개
        DealerHand.RevealFirstCard();

        // 1초 대기
        yield return new WaitForSeconds(1.0f);

        // 16점 이하일 시 딜러 드로우
        while (DealerHand.CalculateScore() <= 16)
        {
            DealerHand.AddCard(_deck.DrawCard());

            // 1초 대기
            yield return new WaitForSeconds(1.0f);
        }

        CalculateResult();
    }

    // 점수 계산
    void CalculateResult()
    {
        int pScore = PlayerHand.CalculateScore();
        int dScore = DealerHand.CalculateScore();

        if (dScore > 21) ProcessResult("Dealer Bust! Player Win!");
        else if (pScore > dScore) ProcessResult("Player Win!");
        else if (pScore < dScore) ProcessResult("Dealer Win...");
        else ProcessResult("Draw");
    }

    // 리절트 표시
    void ProcessResult(string message)
    {
        // 리절트 패널 활성화
        ResultPanel.SetActive(true);
        
        // 메시지 변경
        ResultText.text = message;

        // 액션 패널 끄기
        ActionPanel.SetActive(false);
    }

    // 초기화
    private void Initialize()
    {
        // 판 초기화
        PlayerHand.ClearHand();
        DealerHand.ClearHand();

        // UI 초기화(버튼, 결과창)
        if (ActionPanel != null) ActionPanel.SetActive(true);
        if (ResultPanel != null) ResultPanel.SetActive(false);

        // 덱 초기화
        _deck = new Deck();
        _deck.Initialize();
        _deck.Shuffle();

        // 초기 카드 분배
        Card dealerCard1 = _deck.DrawCard();
        Card dealerCard2 = _deck.DrawCard();

        dealerCard1.IsFaceUp = false;    // 한 장은 뒤집어줘야 함

        DealerHand.AddCard(dealerCard1);
        DealerHand.AddCard(dealerCard2);

        PlayerHand.AddCard(_deck.DrawCard());   // 뒤집을 필요 없으니 바로 삽입
        PlayerHand.AddCard(_deck.DrawCard());
    }

    private void Start()
    {
        if (SoundManager.instance != null && BGM != null) SoundManager.instance.PlayBGM(BGM);
        Initialize();
    }
}