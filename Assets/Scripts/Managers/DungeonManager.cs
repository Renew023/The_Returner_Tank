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
    public int maxWave;

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

    public int GetAliveEnemies()
    {
        return aliveEnemies;
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
        //aliveEnemies += count;
        aliveEnemies = count;
        isWaveInProgress = true;

        // 웨이브 시작 시 호출
        waveMessageUI.ShowMessage($"Wave {currentWave}");

        Debug.Log($"[현재 웨이브: {currentWave}]의 몬스터 수: {count} 스폰!");
    }

    public void OnEnemyDeath()
    {
        aliveEnemies--;
        Debug.Log($"[OnEnemyDeath] 몬스터 처치됨 - 남은 몬스터: {aliveEnemies}");

        // 웨이브 진행 중이고, 살아있는 적이 없으면 웨이브 종료
        if (isWaveInProgress && aliveEnemies <= 0)
        {
            isWaveInProgress = false;
            Debug.Log($"[웨이브 {currentWave}] 완료!");

            if (currentWave >= maxWave)
            {
                Debug.Log($"[DungeonManager] 모든 웨이브 종료!");
                AbsorbExp();
                ClearDungeon();
            }

            else
            {
                // 다음 웨이브로 이동하기 전에 증가시킴
                currentWave++; 
                Debug.Log($"[DungeonManager] 다음 웨이브 {currentWave} 시작!");
                Spawner.instance.SpawnFixedWave();
            }
        }
    }

    void ClearDungeon()
    {
        Debug.Log($"[DungeonManager] 던전 클리어!");

        // 플레이어 HP 저장
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            DataManager.instance.savedPlayerHp = player.CurHP;
            DataManager.instance.savedPlayerMaxHp = player.MaxHp;
        }

        // 던전 레벨 증가
        GameManager.Instance.IncreaseDungeonLevel();

        // 다음 스테이지 인덱스 증가
        GameManager.Instance.currentStageIndex++;

        // 다음 던전 시작을 위한 상태 초기화
        currentWave = 1;
        aliveEnemies = 0;
        isWaveInProgress = false;

        // 마지막 웨이브 끝나면
        waveMessageUI.ShowMessage("CLEAR");
        
        //  warpZone이 null이 아니고 마지막 웨이브 클리어일 때만 활성화 (예외 처리 추가)
        if (warpZone != null)
        {
            warpZone.SetActive(true);
        }
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