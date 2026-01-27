
public class BettingState : IGameState
{
    public void Init(GameManager gm)
    {
        // UI 변경
        UIManager.instance.SetStateUI(GameState.Betting);

        // 판돈 초기화 및 UI 갱신
        gm.Player.ResetBet();
        gm.UpdateMoneyUI();
    }

    public void OnBet(GameManager gm, int amount)
    {
        if (gm.Player.Bet(amount))
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.ChipSound);
            gm.UpdateMoneyUI();
        }
    }

    public void OnDeal(GameManager gm)
    {
        // 베팅하지 않으면 시작하지 않음
        if (gm.Player.BetAmount <= 0) return;

        gm.ChangeState(new PlayingState());
    }

    public void OnHit(GameManager gm) { }

    public void OnStand(GameManager gm) { }
}
