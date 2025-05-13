using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header ("Spawn Points")]
    public Transform[] spawnPoint;
    public float spawnDelay;

    [Header ("Wave 설정")]
    public List<WaveSpawnEntry> waveSpawnEntries;

    //  딕셔너리 자료형을 활용한 레벨에 따른 WaveSpawnData 지정 변수.
    private Dictionary<(int wave, int dungeonLevel), WaveSpawnData> spawnDataDict;

    public int testWave = 1;
    public bool useTestWave = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        spawnPoint = GetComponentsInChildren<Transform>().Where(t => t != this.transform).ToArray();

        BuildSpawnDataDictionary();
    }

    private void BuildSpawnDataDictionary()
    {
        spawnDataDict = new Dictionary<(int wave, int dungeonLevel), WaveSpawnData>();

        foreach (var entry in waveSpawnEntries)
        {
            var key = (entry.wave, entry.dungeonLevel);

            if (!spawnDataDict.ContainsKey(key))
            {
                spawnDataDict.Add(key, entry.data);
            }

            else
            {
                Debug.LogWarning($"[Spawner] 중복된 Key : wave {key.wave}, level {key.dungeonLevel}");
            }
        }
    }

    //  현재 웨이브에 맞는 몬스터들을 무작위로 스폰하는 메서드
    public void SpawnFixedWave()
    {
        // 웨이브 시작 전에 'aliveEnemies'가 0이 아니면 더 이상 스폰하지 않음
        if (DungeonManager.instance.GetAliveEnemies() > 0)
        {
            Debug.LogWarning("웨이브가 끝나지 않았습니다. 적들이 모두 사라질 때까지 기다려주세요.");
            return;
        }

        int wave = useTestWave ? testWave : DungeonManager.instance.currentWave;
        int dungeonLevel = GameManager.Instance.dungeonLevel;

        if (!spawnDataDict.TryGetValue((wave, dungeonLevel), out var data))
        {
            Debug.LogWarning($"[Spawner] wave {wave}, level {dungeonLevel}에 맞는 데이터가 없습니다");
            return;
        }

        //int spawnCount = Mathf.Min(spawnPoint.Length, data.baseEnemyCount + dungeonLevel);
        int spawnCount = Mathf.Min(spawnPoint.Length, data.baseEnemyCount);
        DungeonManager.instance.StartWave(spawnCount);

        for (int i = 0; i < spawnCount; i++)
        {
            int index = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(index);

            if (enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position;
            }
        }

        if (useTestWave)
        {
            DungeonManager.instance.currentWave = testWave;
        }
    }
}

[System.Serializable]
public class WaveSpawnData
{
    public int[] enemyIndices;
    public int baseEnemyCount;
}

[System.Serializable]
public class WaveSpawnEntry
{
    public int wave;
    public int dungeonLevel;
    public WaveSpawnData data;
}