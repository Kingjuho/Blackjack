using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenUI : MonoBehaviour
{
    // 타이틀 씬으로 이동
    public void OnMainButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
