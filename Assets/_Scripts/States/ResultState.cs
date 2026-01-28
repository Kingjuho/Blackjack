public class ResultState : IGameState
{

    private string _msg;        // 메시지
    private int _isWin;        // 승리 여부(0: 승리, 1: 무승부, 2: 패배)
    private bool _isBlackJack;  // 블랙잭 여부

    public ResultState(string msg, int isWin, bool isBlackJack = false)
    {
        _msg = msg;
        _isWin = isWin;
        _isBlackJack = isBlackJack;
    }

    public void Init(GameManager gm)
    {
        // UI 변경
        UIManager.instance.SetStateUI(GameState.Result);
        UIManager.instance.UpdateResultUI(_msg);

        // 승리 시 돈 지급
        if (_isWin == 0) gm.Player.Win(_isBlackJack);
        else if (_isWin == 1) gm.Player.Draw();
        else gm.Player.Lose();

        // UI 갱신
        gm.UpdateMoneyUI();

        // 게임 오버 체크
        if (gm.Player.OwnedMoney <= 0) gm.ChangeState(new GameOverState());
    }

    public void OnBet(GameManager gm, int amount) { }
    public void OnDeal(GameManager gm) { }
    public void OnHit(GameManager gm) { }
    public void OnStand(GameManager gm) { }
}
