using UnityEngine;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager instance;

    public Hand PlayerHand;
    public Hand DealerHand;

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
        Deck deck = new Deck();
        deck.Initialize();
        deck.Shuffle();

        // 딜러
        DealerHand.AddCard(deck.DrawCard());

        // 뒤집기 확인
        Card dealerCard2 = deck.DrawCard();
        dealerCard2.IsFaceUp = true;
        DealerHand.AddCard(dealerCard2);

        // 플레이어
        Card pCard1 = deck.DrawCard();
        pCard1.IsFaceUp = true;
        PlayerHand.AddCard(pCard1);

        Card pCard2 = deck.DrawCard();
        pCard2.IsFaceUp = true;
        PlayerHand.AddCard(pCard2);
    }
}
