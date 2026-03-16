using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 50f;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체에 Enemy 스크립트가 있는지 확인합니다.
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            // 적이라면 데미지를 줍니다.
            enemy.TakeDamage(damage);
        }

        // 무엇에 부딪히든 포탄은 삭제됩니다.
        Destroy(gameObject);
    }
}