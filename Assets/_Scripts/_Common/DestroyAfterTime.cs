using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 1.0f; // 1초 뒤 삭제

    void Start()
    {
        // 생성되자마자 1초 뒤에 자신을 삭제합니다.
        Destroy(gameObject, destroyTime);
    }
}