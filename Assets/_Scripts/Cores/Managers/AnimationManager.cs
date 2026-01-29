using UnityEngine;
using DG.Tweening;


public class AnimationManager : MonoBehaviour
{
    // 싱글톤
    public static AnimationManager instance;

    // 세팅
    [Header("Settings")]
    [SerializeField] float _moveDuration = 0.5f;    // 이동 시간
    
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

    // 카드 분배 애니메이션
    public void DealCard
    (
        Transform card,
        Vector3 startWorldPos,
        Vector3 endLocalPos,
        Transform parent
    )
    {
        // 패가 놓일 영역을 부모로 설정
        card.SetParent(parent);

        // 시작 위치 고정 및 랜덤 회전
        card.position = startWorldPos;
        card.localRotation = Quaternion.Euler(0, 0, Random.Range(-90f, 90f));

        // DoTween 애니메이션 시퀀스 생성
        Sequence seq = DOTween.Sequence();

        // 이동 및 회전
        seq.Join(card.DOLocalMove(endLocalPos, _moveDuration).SetEase(Ease.OutBack));
        seq.Join(card.DOLocalRotate(Vector3.zero, _moveDuration).SetEase(Ease.OutCubic));

        // 효과음 재생
        seq.OnStart(() =>
        {
            if (SoundManager.instance != null) SoundManager.instance.PlaySFX(SoundManager.instance.DealSound);
        });
    }

    // 카드 정렬 애니메이션
    public void AlignCard(Transform card, Vector3 targetLocalPos)
    {
        // DoTween 애니메이션 시퀀스 생성
        Sequence seq = DOTween.Sequence();

        // 이동
        seq.Join(card.DOLocalMove(targetLocalPos, _moveDuration).SetEase(Ease.OutQuad));
    }
}
