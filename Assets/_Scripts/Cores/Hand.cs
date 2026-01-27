using System.Collections.Generic;
using UnityEngine;

// 플레이어, 딜러 부모 클래스
public class Hand : MonoBehaviour
{
    // 패
    protected List<Card> _myCards { get; } = new List<Card>();

    // 카드 생성 위치
    [Header("UI Setting")]
    [SerializeField] protected Transform _cardSpawnPoint;

    // 카드 생성 위치 설정
    public void Setup(Transform spawnPoint)
    {
        _cardSpawnPoint = spawnPoint;
    }

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

    public void AddCard(Card card) 
    { 
        _myCards.Add(card);

        // 안전 장치
        if (_cardSpawnPoint == null) return;

        // 프리팹 생성
        GameObject cardObj = Instantiate(GameManager.instance.CardPrefab, _cardSpawnPoint);

        // 카드 데이터 주입
        CardView view = cardObj.GetComponent<CardView>();
        view.SetCard(card, GameManager.instance.CardAtlas);
    }

    public void ClearHand() 
    { 
        _myCards.Clear();

        // 화면에 있는 카드 전부 삭제
        foreach (Transform child in _cardSpawnPoint) Destroy(child.gameObject);
    }
}
