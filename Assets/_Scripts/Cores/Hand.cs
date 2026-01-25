using System.Collections.Generic;
using UnityEngine;

// 플레이어, 딜러 부모 클래스
public class Hand : MonoBehaviour
{
    // 패
    private List<Card> _myCards = new List<Card>();

    // 점수 계산
    public int CalculateScore()
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in _myCards)
        {
            // 카드값 불러오기
            int val = card.GetValue();
            
            // 에이스면 일단 1점 더해주고 카운팅
            if (card.Rank == Rank.Ace) aceCount++;

            total += val;
        }

        // 에이스를 11로 사용할 수 있는 경우 자동 계산
        while (aceCount > 0 && total <= 11)
        {
            total += 10;
            aceCount--;
        }

        return total;
    }

    public void AddCard(Card card) { _myCards.Add(card); }

    public void ClearHand() { _myCards.Clear(); }
}
