using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header ("Spawn Points")]
    public Transform[] spawnPoint;
    public float spawnDelay = 1.0f;

    [Header ("Wave ����")]
    public List<WaveSpawnEntry> waveSpawnEntries;

    //  ��ųʸ� �ڷ����� Ȱ���� ������ ���� WaveSpawnData ���� ����.
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
                Debug.LogWarning($"[Spawner] �ߺ��� Key : wave {key.wave}, level {key.dungeonLevel}");
            }
        }
    }

    //  ���� ���̺꿡 �´� ���͵��� �������� �����ϴ� �޼���
    public void SpawnFixedWave()
    {
        //  �׽�Ʈ������ ����� ���̺긦 ����Ұ��� üũ
        int wave = useTestWave ? testWave : DungeonManager.instance.currentWave;

        //  ���� ������ ���� ���� ��������
        int dungeonLevel = GameManager.Instance.dungeonLevel;

        if(!spawnDataDict.TryGetValue((wave, dungeonLevel), out var data))
        {
            Debug.LogWarning($"[Spawner] wave {wave}, level {dungeonLevel}�� �´� �����Ͱ� �����ϴ�");
            return;
        }

        int spawnCount = Mathf.Min(spawnPoint.Length, data.baseEnemyCount + dungeonLevel);
        DungeonManager.instance.StartWave(spawnCount);

        for (int i = 0; i < spawnCount; i++)
        {
            int index = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(index);

            if(enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position; 
            }
        }

        if(useTestWave)
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