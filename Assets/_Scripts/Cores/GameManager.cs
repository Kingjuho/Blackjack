using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager instance;

    // 딜러, 플레이어
    public Hand PlayerHand;
    public Hand DealerHand;

    // 덱
    Deck _deck;

    [Header("Resources")]
    public GameObject CardPrefab;
    public SpriteAtlas CardAtlas;

    [Header("UI References")]
    public GameObject ActionPanel;
    public GameObject CoinPanel;
    public GameObject ResultPanel;

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

        if (score > 21)
        {
            Debug.Log($"Player Bust! 패배");
            ActionPanel.SetActive(false);
            // TODO: 리절트 창
        }
    }

    // 스탠드 버튼
    public void OnStandButton()
    {
        Debug.Log("Player Stand!");
        ActionPanel.SetActive(false);

        // 딜러 자동 프로세스 시작
        StartCoroutine(DealerProcess());
    }

    // 딜러 자동 프로세스
    IEnumerator DealerProcess()
    { 
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

    // 리절트 표시
    void CalculateResult()
    {
        int pScore = PlayerHand.CalculateScore();
        int dScore = DealerHand.CalculateScore();

        Debug.Log($"[Finish] Player: {pScore} vs Dealer: {dScore}");

        if (dScore > 21) Debug.Log("Dealer Bust! Player Win!");
        else if (pScore > dScore) Debug.Log("Player Win!");
        else if (pScore < dScore) Debug.Log("Dealer Win...");
        else Debug.Log("Draw.");
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
        Initialize();
    }
}