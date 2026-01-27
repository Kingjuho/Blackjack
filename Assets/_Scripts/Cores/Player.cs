using UnityEngine;

public class Player : Hand
{
    // 현재 소지금
    public int OwnedMoney { get; private set; } = 1000;
    // 판돈
    public int BetAmount { get; private set; } = 0;

    // 베팅
    public bool Bet(int amount)
    {
        // 돈이 부족하면 false
        if (OwnedMoney < amount) return false;

        OwnedMoney -= amount;
        BetAmount += amount;
        return true;
    }

    // 베팅 초기화
    public void ResetBet()
    {
        OwnedMoney += BetAmount;
        BetAmount = 0;
    }

    // 승리 정산
    public void Win(bool isBlackJack)
    {
        // 블랙잭이면 1.5배
        int profit = isBlackJack ? (int)(BetAmount * 1.5) : BetAmount;
        OwnedMoney += (BetAmount + profit);
        BetAmount = 0;
    }

    // 패배 정산
    public void Lose() { BetAmount = 0; }

    // 무승부 정산
    public void Draw()
    {
        OwnedMoney += BetAmount;
        BetAmount = 0;
    }
}
