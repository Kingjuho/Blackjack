public class PlayingState : IGameState
{
   public void Init(GameManager gm)
    {
        // UI 변경
        UIManager.instance.SetStateUI(GameState.Playing);
    }

    public void OnBet(GameManager gm, int amount) { }
    public void OnDeal(GameManager gm) { }
    
    public void OnHit(GameManager gm)
    {
        // 패에 카드 1장 추가
        gm.Player.AddCard(gm.Deck.DrawCard(), gm.DeckPosition);

        // 점수 체크
        int score = gm.Player.CalculateScore();

        // 블랙잭/버스트 체크
        if (score > 21) gm.ChangeState(new ResultState($"Bust!\nYou Lose..", 2));
        else if (score == 21) OnStand(gm);
    }

    public void OnStand(GameManager gm)
    {
        gm.StartCoroutine(gm.DealerProcess());
    }
}
