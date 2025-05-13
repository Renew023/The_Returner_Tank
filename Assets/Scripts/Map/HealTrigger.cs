// Assets/Scripts/Map/HealTrigger.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealTrigger : MonoBehaviour
{
    [Header("회복량 ")]
    public int healAmount = 20;

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
        if (!other.CompareTag("Player")) return;

        var player = other.GetComponent<Player>();
        if (player == null) return;

        player.HealTrigger(healAmount);

        Destroy(gameObject);
    }
}
