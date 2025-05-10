using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [Header("Heal Prefab")]
    public GameObject healPrefab;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = new Vector3(0f, 5f, 0f);

    void Start()
    {
        // �� ���� �� �� ���� ����
        Instantiate(healPrefab, spawnPosition, Quaternion.identity);
    }
}
