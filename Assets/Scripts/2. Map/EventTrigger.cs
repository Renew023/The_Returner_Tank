using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    bool hasTriggered = false;
    void Awake()
    {
        // 트리거용 설정 보장
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;

        var rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;                // ▼ 이미 실행된 적 있으면 무시
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;                     // ▼ 재진입 방지
        GetComponent<Collider2D>().enabled = false;

        var player = other.GetComponent<Player>();
        if (player != null) player.LevelUpTrigger(99999);

        Destroy(gameObject, 0.1f);               // ▼ 약간 딜레이 주면 중첩 호출 방지 확실
    }
}
