using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawn Points")]
    public Transform[] spawnPoint;

    //  ���̵� ���� ������
    public List<WaveSpawnData> allDifficultyData;

    [Header ("Wave Spawn ����")]
    public WaveSpawnData[] waveSpawnData;      //  ����Ƽ ������ �󿡼� wave ����

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

        else
        {
            Destroy(gameObject);
        }
    }

    //  ���� ���̺꿡 �´� ���͵��� �������� �����ϴ� �޼���
    public void SpawnFixedWave(int wave)
    {
        WaveSpawnData data = waveSpawnData.FirstOrDefault(w => w.wave == wave);

        //  GameManager �� ���� �������� ��ȣ�� �ҷ��´�.
        //int stageIndex = GameManager.instance.currentStageIndex;

        //  ���� ���������� �´� ���̵� ������ ã��
        //WaveSpawnData data = allDifficultyData.FirstOrDefault(d => stageIndex >= d.minStageIndex && stageIndex <= d.maxStageIndex);

        if (data == null)
        {
            Debug.LogWarning($"[Spawner] Wave {wave} ���� ����");
            //Debug.LogWarning($"[Spawner] �ش� �������� ({stageIndex})�� ���̵� �����Ͱ� �������� �ʽ��ϴ�");
            return;
        }
    }

    //  ���͵� ���� ���
    void SpawnEnemis()
    {
        GameObject enemy = DungeonManager.instance.pools.CreateEnemies(Random.Range(0, 4));

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