// Assets/Scripts/Map/HealTrigger.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealTrigger : MonoBehaviour
{
    [Header("ȸ���� (���� ����)")]
    public int healAmount = 20;

    void Awake()
    {
        // Ʈ���ſ� ���� ����
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

        // HealEventManager���� �� ���� �˸���
        if (HealEventManager.Instance != null)
        {
            HealEventManager.Instance.OnHealCollected(gameObject);
        }
        else
        {
            Debug.LogWarning("HealTrigger: HealEventManager�� ���� �����ϴ�!");
            Destroy(gameObject);
        }
    }
}
