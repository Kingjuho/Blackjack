using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CardView : MonoBehaviour
{
    [Header("UI Conponents")]
    [SerializeField] Image _frontImage;
    [SerializeField] Image _backImage;

    public void SetCard(Card cardData, SpriteAtlas atlas)
    {
        // 아틀라스에서 스프라이트 가져오기
        string spriteName = $"{cardData.Rank}_of_{cardData.Suit}".ToLower();

        Sprite sprite = atlas.GetSprite(spriteName);
        if (sprite != null)
        {
            _frontImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"이미지를 찾을 수 없음: {spriteName}");
        }

        // 앞뒷면 처리
        _backImage.gameObject.SetActive(!cardData.IsFaceUp);
    }
}
