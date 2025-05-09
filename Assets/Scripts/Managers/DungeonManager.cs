using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    //  ���� ���̺� ������
    public int currentWave = 1;

    //  ���̺� �� ���� ����ִ� ������ �� ������
    private int aliveEnemies = 0;

    //  ���̺갡 ���� ������ �����ϱ� ���� ����
    private bool isWaveInProgress = false;

    public Dungeon pools;           //  ������Ʈ Ǯ�� �Ŵ���

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
    }

    //  ���� ���̺� ���� �� ���� �� ���� �޼���
    public void StartWave(int count)
    {
        aliveEnemies = count;
        isWaveInProgress = true;
        Debug.Log($"[���̺� {currentWave}] ���� {count} ���� ��ȯ!");
    }


    //  ���Ͱ� �׾��� �� ȣ��Ǵ� �޼���
    public void OnEnemyDeath()
    {
        //  ���� ���� �� ����
        aliveEnemies--;

        //  ���̺� �� ��� ���Ͱ� �׾��ٸ�, ���� ���̺� ���� �� ���� ���̺� ����
        if(isWaveInProgress && aliveEnemies <= 0)
        {
            isWaveInProgress = false;
            Debug.Log($"[���̺� {currentWave}] �Ϸ�!");
            NextWave();
        }
    }

    //  ���� ���̺� ȣ�� �޼���
    void NextWave()
    {
        //  ���̺� ����
        currentWave++;

        Debug.Log($"[DungeonManager] ���� ���̺�: {currentWave}");

        Spawner.instance.SpawnFixedWave(currentWave);
    }

    private void Update()
    {
        //  ���̺� ����
        //currentWave++;

        //Debug.Log($"[DungeonManager] ���� ���̺�: {currentWave}");
    }
}