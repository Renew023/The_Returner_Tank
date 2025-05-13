using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Transform expParent; // ExpObjects 오브젝트
    [SerializeField] private Transform player;    // 플레이어 Transform
    [SerializeField] private GameObject warpZone;
    [SerializeField] private WaveMessageUI waveMessageUI;
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

    private void OnEnable()
    {
        UIManager.Instance.uiController.SetDungeonUI(true);
    }

    private void OnDisable()
    {
        UIManager.Instance.uiController.SetDungeonUI(false);
    }

    private void Start()
    {
        //  DungeonManager���� ���� ���̺긦 �����Ѵ�!!
        currentWave = 1;

        Spawner.instance.SpawnFixedWave();
    }

    //  ���� ���̺� ���� �� ���� �� ���� �޼���
    public void StartWave(int count)
    {
        aliveEnemies += count;
        isWaveInProgress = true;

        // 웨이브 시작 시 호출
        waveMessageUI.ShowMessage($"Wave {currentWave}");

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
                AbsorbExp();
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

        // 마지막 웨이브 끝나면
        waveMessageUI.ShowMessage("CLEAR");

        //  �������� ���� ȭ������ ���Ϳ� ������ ����
        warpZone.SetActive(true);
    }
    
    private void AbsorbExp()
    {
        if (expParent == null)
            expParent = GameObject.Find("ExpObjects")?.transform;

        if (player == null)
            player = GameObject.Find("Player")?.transform;

        if (expParent == null || player == null) return;

        foreach (Transform exp in expParent)
        {
            ExpObject expObj = exp.GetComponent<ExpObject>();
            if (expObj != null)
            {
                expObj.StartAbsorb(player);
            }
        }
    }
}