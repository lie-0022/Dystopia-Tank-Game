using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 다시 시작 기능을 위해 추가

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Reference")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel; // 유니티에서 패널을 연결할 칸

    private int currentScore = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
    }

    // 플레이어가 죽으면 이 함수를 실행할 겁니다.
    public void GameOver()
    {
        // 1. 숨겨놨던 패널을 다시 켭니다.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2. 마우스 커서를 자유롭게 움직일 수 있게 풉니다.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 버튼을 눌렀을 때 실행될 재시작 함수
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}