using UnityEngine;


public class HealTrigger : MonoBehaviour
{
    [Header("Heal Amount (���߿� ����)")]
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

        // TODO: ���� ü�� ȸ�� ���� �߰�
        Debug.Log($"HealTrigger: �÷��̾� ü�� +{healAmount}");

        Destroy(gameObject);
    }
}
