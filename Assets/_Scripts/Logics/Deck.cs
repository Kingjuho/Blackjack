using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    // 덱
    private Stack<Card> _cards = new Stack<Card>();

    // 덱 생성
    public void Initialize()
    {
        // enum을 foreach문으로 사용하여 총 52장 생성
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Push(new Card(suit, rank));
            }    
        }
    }

    // 덱 셔플
    public void Shuffle()
    {
        // 셔플을 위해 리스트로 변환
        List<Card> cardList = _cards.ToList();

        // TODO: 피셔 예이츠 셔플 알고리즘 사용

        // 리스트를 다시 스택으로 변환하여 원본과 교체
        _cards = new Stack<Card>(cardList);
    }

    // 덱에서 카드 드로우
    public Card DrawCard()
    {
        return _cards.Count > 0 ? _cards.Pop() : null;
    }
}
