using UnityEngine;
using UnityEngine.UI; // 슬라이더 제어를 위해 필수

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Reference")]
    public Slider hpSlider;

    void Start()
    {
        currentHealth = maxHealth;

        // 시작할 때 HP 바 초기화
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // 0 밑으로 내려가지 않게 방지
        if (currentHealth < 0) currentHealth = 0;

        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        Debug.Log("플레이어 남은 체력: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망!");

        // 1. 죽을 때 UI를 0으로 확실히 처리
        if (hpSlider != null)
        {
            hpSlider.value = 0;
            hpSlider.fillRect.gameObject.SetActive(false);
        }

        // 2. 카메라를 부모(탱크)로부터 독립시켜 화면 꺼짐 방지
        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(null);
        }

        // 3. GameManager에게 게임 오버 알림
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
        }

        // 4. 탱크 오브젝트 비활성화
        transform.root.gameObject.SetActive(false);
    }
}