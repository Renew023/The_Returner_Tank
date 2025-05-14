// Assets/Scripts/Map/HealEventManager.cs
using UnityEngine;

public class HealEventManager : MonoBehaviour
{
    public static HealEventManager Instance { get; private set; }

    void Awake()
    {
        // 싱글톤 패턴으로 인스턴스 보관
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void OnEnable()
    {
        UIManager.Instance.uiController.SetDungeonUI(true);
    }

    private void OnDisable()
    {
        UIManager.Instance.uiController.SetDungeonUI(false);
    }

    /// 플레이어가 힐 아이템을 먹었을 때 호출
    public void OnHealCollected(GameObject healObject)
    {
        // 1) 힐 아이템 사라지기
        Destroy(healObject);

        // 2) 로그나 이펙트 재생 (나중에 HP 회복 로직 추가)

    }
}
