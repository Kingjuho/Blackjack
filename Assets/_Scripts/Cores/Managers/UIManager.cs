using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // 싱글톤
    public static UIManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject _coinPanel;   // 코인 패널
    [SerializeField] private GameObject _actionPanel; // 히트/스탠드 패널
    [SerializeField] private GameObject _resultPanel; // 결과 패널

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI _ownedMoneyText;   // 현재 소지금
    [SerializeField] private TextMeshProUGUI _betAmountText;    // 판돈
    [SerializeField] private TextMeshProUGUI _resultText;       // 리절트

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 상태에 따른 패널 활성화 여부 변경
    public void SetStateUI(GameState state)
    {
        _coinPanel.SetActive(state == GameState.Betting);
        _actionPanel.SetActive(state == GameState.Playing);
        _resultPanel.SetActive(state == GameState.Result);
    }

    // 돈 텍스트 갱신
    public void UpdateMoneyUI(int ownedMoney, int betAmount)
    {
        _ownedMoneyText.text = $"Cash: {ownedMoney}";
        _betAmountText.text = $"Bet: {betAmount}";
    }

    // 리절트 텍스트 갱신
    public void UpdateResultUI(string message)
    {
        _resultText.text = message;
    }
}

// 현재 게임 상태
public enum GameState
{
    Betting,
    Playing,
    Result
}
