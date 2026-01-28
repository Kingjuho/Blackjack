using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    // 게임 씬으로 이동
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    // 에디터면 플레이 모드 종료, 빌드 버전은 진짜 종료
    public void OnExitButton()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
