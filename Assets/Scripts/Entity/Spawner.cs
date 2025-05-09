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

    [Header ("Wave Spawn ����")]
    public WaveSpawnData[] waveSpawnData;      //  ����Ƽ ������ �󿡼� wave ����

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
            Debug.LogWarning($"[Spawner] Wave {wave} ���� ����");
            return;
        }

        //  ��ȯ�� ���� �� = ���� �������� ����ŭ ��ȯ �� 4���� ���� ��ġ�� ��ȯ
        int spawnCount = Mathf.Min(spawnPoint.Length, 4);
        DungeonManager.instance.StartWave(spawnCount);

        //  �� ���� ��ġ�� �ش� ���͸� ��ȯ
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

    //  Enemies �迭���� � ���͸� ���� �ε����� ���
    public int[] enemyIndices;
}