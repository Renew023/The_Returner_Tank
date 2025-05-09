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

    [Header ("Wave Spawn 설정")]
    public WaveSpawnData[] waveSpawnData;      //  유니티 에디터 상에서 wave 설정

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>()
                     .Where(t => t != this.transform).ToArray();

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnFixedWave(int wave)
    {
        WaveSpawnData data = waveSpawnData.FirstOrDefault(w => w.wave == wave);

        if(data == null)
        {
            Debug.LogWarning($"[Spawner] Wave {wave} 설정 없음");
            return;
        }

        //  소환할 몬스터 수 = 스폰 포지션의 수만큼 소환 → 4개의 스폰 위치에 소환
        int spawnCount = Mathf.Min(spawnPoint.Length, 4);
        DungeonManager.instance.StartWave(spawnCount);

        //  각 스폰 위치에 해당 몬스터를 소환
        for(int i = 0; i < spawnCount; i++)
        {
            int randomEnemyIndex = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(randomEnemyIndex);

            if(enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position;

                enemy.GetComponent<Enemy>().Initialize();
            }
        }

    }

    private void Start()
    {
        SpawnFixedWave(DungeonManager.instance.currentWave);
    }

    private void Update()
    {
            
    }
}

[System.Serializable]
public class WaveSpawnData
{
    public int wave;

    //  Enemies 배열에서 어떤 몬스터를 쓸지 인덱스로 명시
    public int[] enemyIndices;
}