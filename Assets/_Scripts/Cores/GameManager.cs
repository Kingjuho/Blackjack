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

        // 딜러 2장
        DealerHand.AddCard(deck.DrawCard());
        DealerHand.AddCard(deck.DrawCard());

        // 플레이어 2장
        PlayerHand.AddCard(deck.DrawCard());
        PlayerHand.AddCard(deck.DrawCard());
    }
}
