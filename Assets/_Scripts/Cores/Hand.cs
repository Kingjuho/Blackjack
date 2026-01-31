using System.Collections.Generic;
using UnityEngine;

// 플레이어, 딜러 부모 클래스
public class Hand : MonoBehaviour
{
    // 패
    protected List<Card> _myCards { get; } = new List<Card>();

    // 블랙잭 여부
    public bool IsBlackJack => CalculateScore() == 21 && _myCards.Count == 2;

    // 카드 UI 세팅
    [Header("UI Setting")]
    [SerializeField] protected Transform _cardSpawnPoint;
    [SerializeField] float _spacing = 60f;

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

    // 카드 추가
    public void AddCard(Card card, Transform startPos)
    {
        _myCards.Add(card);

        // 안전 장치
        if (_cardSpawnPoint == null) return;

        // 프리팹 생성
        GameObject cardObj = Instantiate(GameManager.instance.CardPrefab);

        // 카드 데이터 주입
        CardView view = cardObj.GetComponent<CardView>();
        view.SetCard(card, GameManager.instance.CardAtlas);

        // 카드 정렬
        AlignCard(cardObj.transform, startPos);
    }

    // 카드 정렬
    void AlignCard(Transform newCardTr, Transform startPos)
    {
        int count = _myCards.Count;

        // 전체 카드의 너비 계산
        float totalWidth = (count - 1) * _spacing;

        // 시작 지점
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            // 해당 카드의 목표 지점
            float targetX = startX + (i * _spacing);
            Vector3 targetLocalPos = new Vector3(targetX, 0, 0);

            // 리스트에서 카드 가져오기
            Transform cardTr;
            if (i == count - 1)
            {
                cardTr = newCardTr;

                // 새 카드는 애니메이션 호출
                AnimationManager.instance.DealCard(
                    cardTr,
                    startPos.position,
                    targetLocalPos,
                    _cardSpawnPoint
                );
            }
            else
            {
                // 기존 카드는 옆으로 살짝 이동
                AnimationManager.instance.AlignCard(_cardSpawnPoint.GetChild(i), targetLocalPos);
            }
        }
    }

    // 카드 청소
    public void ClearHand() 
    { 
        _myCards.Clear();

        // 화면에 있는 카드 전부 삭제
        foreach (Transform child in _cardSpawnPoint) Destroy(child.gameObject);
    }
}
