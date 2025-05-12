using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    [SerializeField] private GameObject warpZone;
    public static DungeonManager instance;

    //  ���� ���̺� ������
    public int currentWave = 1;
    public int maxWave = 3;

    //  ���̺� �� ���� ����ִ� ������ �� ������
    private int aliveEnemies = 0;

    //  ���̺갡 ���� ������ �����ϱ� ���� ����
    private bool isWaveInProgress = false;

    public Dungeon pools;           //  ������Ʈ Ǯ�� �Ŵ���

    private void Awake()
    {
        if (warpZone != null)
            warpZone.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //  DungeonManager���� ���� ���̺긦 �����Ѵ�!!
        currentWave = 1;

        Spawner.instance.SpawnFixedWave();
        UIManager.Instance.uiController.SetDungeonUI(true);
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
            Debug.Log($"���̺� {currentWave}] �Ϸ�!");

            if(currentWave >= maxWave)
            {
                Debug.Log($"[DungeonManager] ��� ���̺� ����!");
                ClearDungeon();
            }

            else
            {
                Debug.Log($"[DungeonManager] ���� ���̺� {currentWave}] ����!");
                //NextWave();

                //  ���̺� ����
                currentWave++;

                Debug.Log($"[DungeonManager] ���� ���̺�: {currentWave}");

                Spawner.instance.SpawnFixedWave();
            }

        }
    }

    void ClearDungeon()
    {
        Debug.Log($"[DungeonManager] ���� Ŭ����!");

        //  ���� �÷��̾� HP ����
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            DataManager.instance.savedPlayerHp = player.CurHP;
            DataManager.instance.savedPlayerMaxHp = player.MaxHp;
        }

        //  ���� ���̵� ����
        GameManager.Instance.IncreaseDungeonLevel();

        //  ���� �������� �ε��� ���� (���� �������� �����)
        GameManager.Instance.currentStageIndex++;

        //  ���� ������ ���� �ʱ�ȭ
        currentWave = 1;

        //  �������� ���� ȭ������ ���Ϳ� ������ ����
        warpZone.SetActive(true);
    }
}