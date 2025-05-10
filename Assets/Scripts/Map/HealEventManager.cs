using System.Collections.Generic;
using UnityEngine;

public class HealEventManager : MonoBehaviour
{
    [Header("Heal Prefab & Points")]
    public GameObject healPrefab;             // 힐 아이콘/이펙트 프리팹
    public List<Transform> spawnPoints;       // DungeonManager의 enemySpawnPoints 그대로 재사용

    [Header("Camera")]
    public FollowCamera followCamera;         // 카메라 스크립트

    private void Start()
    {
        SpawnHeals();

        // 카메라 초기 팬 동작이 이미 FollowCamera에 있다면 안 건드려도 됩니다.
        if (followCamera != null)
            followCamera.BeginPan();
    }

    private void SpawnHeals()
    {
        // 기존 DungeonManager에서 몬스터를 spawn 하던 루프만 힐로 교체
        foreach (var pt in spawnPoints)
        {
            Instantiate(healPrefab, pt.position, Quaternion.identity);
        }
    }

    // 필요하다면 Update() 나 종료 로직 등 복사 붙여넣기…
}
