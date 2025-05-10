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

    [Header ("���̵� ����")]
    public WaveSpawnData[] waveSpawnData;      //  ����Ƽ ������ �󿡼� wave ����

    public int testWave = 1;
    public bool useTestWave = false;

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
            //  �����Ѵ�
            Destroy(gameObject);
        }
    }

    //  ���� ���̺꿡 �´� ���͵��� �������� �����ϴ� �޼���
    public void SpawnFixedWave()
    {
        int wave = useTestWave ? testWave : DungeonManager.instance.currentWave;

        //  ���� ������ ���� ���� ��������
        int dungeonLevel = GameManager.Instance.dungeonLevel;

        //  ���̵� ���� ��������
        WaveSpawnData data = waveSpawnData
        .FirstOrDefault(d => d.wave == wave && dungeonLevel >= d.minStageIndex && dungeonLevel <= d.maxStageIndex);

        if (data == null)
        {
            Debug.LogWarning($"[Spawner] ���� ���� ({dungeonLevel})�� �´� ���̵� ������ �����ϴ�");
            return;
        }

        //  ��ȯ�� ���� �� = Inspector â���� ������ enemyIndices�� ����ŭ ������ ���� ��ġ���� �����Ѵ�.
        int spawnCount = Mathf.Min(spawnPoint.Length, data.enemyIndices.Length);

        DungeonManager.instance.StartWave(spawnCount);

        //  �� ���� ��ġ�� �ش� ���͸� ��ȯ
        for(int i = 0; i < spawnCount; i++)
        {
            int randomnemyIndex = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(randomnemyIndex);

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

    private void Start()
    {
        SpawnFixedWave();
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

    //  ���̵� ���� ���� �������� ��ȣ
    public int minStageIndex;

    //  ���̵� ���� �� �������� ��ȣ
    public int maxStageIndex;
}