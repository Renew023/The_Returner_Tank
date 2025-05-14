// Assets/Scripts/Map/HealTrigger.cs
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class HealTrigger : MonoBehaviour
{
    bool hasTriggered = false;

    [Header("회복량 ")]
    public int healAmount = 50;

    void Awake()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;
        hasTriggered = true;
        GetComponent<Collider2D>().enabled = false;

        var player = other.GetComponent<Player>();
        player?.HealTrigger(healAmount);
        Destroy(gameObject, 0.1f);

        // 언로드 전에 Collider 차단
        SceneManager.UnloadSceneAsync("Event_HealScene");
    }
}
