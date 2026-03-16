using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health & Score")]
    public float health = 100f;          // 적의 체력
    public int scoreValue = 10;         // 처치 시 얻을 점수
    public float collisionDamage = 20f; // 플레이어와 부딪혔을 때 줄 데미지

    [Header("Movement")]
    public float moveSpeed = 3.0f;      // 이동 속도
    private Transform playerTransform;   // 플레이어 위치 추적용

    [Header("Effects")]
    public GameObject explosionPrefab;  // 죽을 때 소환할 폭발 프리팹

    void Start()
    {
        // "Player" 태그를 가진 오브젝트를 찾아 위치를 가져옵니다.
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // 플레이어가 살아있다면 플레이어를 향해 이동합니다.
        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌했는지 확인합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHP = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHP != null)
            {
                // 플레이어에게 데미지를 줍니다.
                playerHP.TakeDamage(collisionDamage);

                // 플레이어와 부딪히면 적 자신도 즉시 죽습니다. (폭발 효과 포함)
                Die();
            }
        }
    }

    // 포탄에 맞았을 때 호출되는 함수
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 1. 적의 현재 위치에 폭발 효과 프리팹을 소환합니다.
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 2. GameManager를 통해 점수를 추가합니다.
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(scoreValue);
        }

        // 3. 적 오브젝트를 씬에서 삭제합니다.
        Destroy(gameObject);
    }
}