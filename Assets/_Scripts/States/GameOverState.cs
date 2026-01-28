using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : IGameState
{

    public void Init(GameManager gm)
    {
        // 씬 전환
        SceneManager.LoadScene("GameOver");

        // 싱글톤 파괴
        DestroySingleton();
    }

    public void OnBet(GameManager gm, int amount) { }
    public void OnDeal(GameManager gm) { }
    public void OnHit(GameManager gm) { }
    public void OnStand(GameManager gm) { }

    // 씬 내부 객체를 참조 중인 싱글톤 파괴 함수
    void DestroySingleton()
    {
        if (GameManager.instance != null) Object.Destroy(GameManager.instance.gameObject);
        if (UIManager.instance != null) Object.Destroy(UIManager.instance.gameObject);

        // SoundManager는 파괴하면 안 됨
    }
}
