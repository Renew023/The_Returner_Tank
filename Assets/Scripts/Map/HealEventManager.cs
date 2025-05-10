using System.Collections.Generic;
using UnityEngine;

public class HealEventManager : MonoBehaviour
{
    [Header("Heal Prefab & Points")]
    public GameObject healPrefab;             // �� ������/����Ʈ ������
    public List<Transform> spawnPoints;       // DungeonManager�� enemySpawnPoints �״�� ����

    [Header("Camera")]
    public FollowCamera followCamera;         // ī�޶� ��ũ��Ʈ

    private void Start()
    {
        SpawnHeals();

        // ī�޶� �ʱ� �� ������ �̹� FollowCamera�� �ִٸ� �� �ǵ���� �˴ϴ�.
        if (followCamera != null)
            followCamera.BeginPan();
    }

    private void SpawnHeals()
    {
        // ���� DungeonManager���� ���͸� spawn �ϴ� ������ ���� ��ü
        foreach (var pt in spawnPoints)
        {
            Instantiate(healPrefab, pt.position, Quaternion.identity);
        }
    }

    // �ʿ��ϴٸ� Update() �� ���� ���� �� ���� �ٿ��ֱ⡦
}
