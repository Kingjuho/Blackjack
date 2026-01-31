using UnityEngine;

public class Dealer : Hand
{
    // 딜러의 첫 번째 카드 공개
    public void RevealFirstCard()
    {
        // 뒤집어져 있는 카드는 무조건 첫 번째 카드
        if (_myCards.Count > 0) _myCards[0].IsFaceUp = true;

        // 첫 번째 카드만 다시 로드
        Transform firstCardObj = _cardSpawnPoint.GetChild(0);
        if (firstCardObj != null)
        {
            firstCardObj.GetComponent<CardView>().SetCard(_myCards[0], GameManager.instance.CardAtlas);
            
            // 효과음 재생
            if (SoundManager.instance != null) 
                SoundManager.instance.PlaySFX(SoundManager.instance.FlipSound);
        }
    }
}
