using UnityEngine;
using UnityEngine.SceneManagement;

public class HealSpawner : MonoBehaviour
{
    public static HealSpawner Instance { get; private set; }

    [Header("히일 프리팹")]
    public GameObject HealPrefab;

    [Header("스폰 위치")]
    public Vector3 spawnPosition = new Vector3(0f, 5f, 0f);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        var HealPrefab = Resources.Load<GameObject>("HealPrefab");
        if (HealPrefab != null)
        {
            // 월드 좌표 (0,5,0)에 스폰
            UnityEngine.Object.Instantiate(
                HealPrefab,
                new Vector3(0f, 5f, 0f),
                Quaternion.identity
            );
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Event_HealScene")
            SpawnHeal();
    }

    private void SpawnHeal()
    {
        if (HealPrefab == null)
        {
            Debug.LogError("HealSpawner: healPrefab 이 할당되지 않았습니다!");
            return;
        }
        // 혹시 남은 힐이 있으면 삭제
        var old = GameObject.FindWithTag("Heal");
        if (old != null) Destroy(old);

        Instantiate(HealPrefab, spawnPosition, Quaternion.identity);
    }
}