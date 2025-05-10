using UnityEngine;


public class HealTrigger : MonoBehaviour
{
    [Header("Heal Amount (나중에 연결)")]
    public int healAmount = 20;

    private void Awake()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
        }

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // TODO: 실제 체력 회복 로직 추가
        Debug.Log($"HealTrigger: 플레이어 체력 +{healAmount}");

        Destroy(gameObject);
    }
}
