using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [Header("Heal Prefab")]
    public GameObject healPrefab;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = new Vector3(0f, 5f, 0f);

    void Start()
    {
        // 씬 시작 시 한 번만 생성
        Instantiate(healPrefab, spawnPosition, Quaternion.identity);
    }
}
