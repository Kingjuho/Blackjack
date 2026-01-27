public interface IGameState
{
    // 초기화
    void Init(GameManager gm);
    // 칩 버튼 클릭
    void OnBet(GameManager gm, int amount);
    // 베팅 완료 버튼 클릭
    void OnDeal(GameManager gm);
    // 히트 버튼 클릭
    void OnHit(GameManager gm);
    // 스탠드 버튼 클릭
    void OnStand(GameManager gm);
}
