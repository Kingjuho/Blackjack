// 모양
public enum Suit { Spades, Hearts, Diamonds, Clubs }
// 숫자
public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }
    // 스프라이트 이름
    public string SpriteName
    {
        get
        {
            string rankName = Rank switch
            {
                Rank.Ace => "ace",
                Rank.Jack => "jack",
                Rank.Queen => "queen",
                Rank.King => "king",
                _ => ((int)Rank).ToString(),
            };

            return $"{rankName}_of_{Suit}_0".ToLower();
        }
    }

    // 카드 뒤집혀있음 여부
    public bool IsFaceUp { get; set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public int GetValue()
    {
        // Jack 위론 다 10, Ace는 따로 계산
        if (Rank >= Rank.Jack) return 10;
        return (int)Rank;
    }
}