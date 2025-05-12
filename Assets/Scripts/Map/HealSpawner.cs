using UnityEngine;
using UnityEngine.SceneManagement;

public class HealSpawner : MonoBehaviour
{
    public static HealSpawner Instance { get; private set; }

    [Header("���� ������")]
    public GameObject HealPrefab;

    [Header("���� ��ġ")]
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
            Debug.LogError("HealSpawner: healPrefab �� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }
        // Ȥ�� ���� ���� ������ ����
        var old = GameObject.FindWithTag("Heal");
        if (old != null) Destroy(old);

        Instantiate(HealPrefab, spawnPosition, Quaternion.identity);
    }
}