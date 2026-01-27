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

    public void OnHitButton()
    {

    }
    
    public void OnStandButton()
    {

    }

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