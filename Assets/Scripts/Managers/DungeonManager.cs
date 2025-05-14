using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Transform expParent; // ExpObjects 오브젝트
    [SerializeField] private Transform player;    // 플레이어 Transform
    [SerializeField] private GameObject warpZone;
    //[SerializeField] private UIController uiController;
    [SerializeField] private WaveMessageUI waveMessageUI;
    public static DungeonManager instance;

    //  현재 웨이브 번호
    public int currentWave = 1;
    
    //  최대 웨이브 수
    public int maxWave;

    //  현재 웨이브에서 살아있는 적의 수
    private int aliveEnemies = 0;

    //  현재 웨이브가 진행 중인지 여부 체크용 변수
    private bool isWaveInProgress = false;

    public Dungeon pools;           

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
        currentWave = 1;
        
        //  던전 씬에서 첫번째 웨이브 스폰 
        Spawner.instance.SpawnFixedWave();

        StartCoroutine(InitializeAfterUIReady());
    }

    private IEnumerator InitializeAfterUIReady()
    {
        yield return null; // 한 프레임 기다림 (UIController의 OnEnable()이 먼저 실행되도록)

        if (UIManager.Instance.uiController != null)
        {
            UIManager.Instance.uiController.SetDungeonUI(true);
            //uiController.SetDungeonUI(true);
        }
        else
        {
            Debug.LogWarning("UIController가 아직 초기화되지 않았습니다.");
        }

        Spawner.instance.SpawnFixedWave();
    }

    //  웨이브 시작 시 호출, 살아있는 적의 수를 설정하는 메서드
    public void StartWave(int count)
    {
        aliveEnemies = count;
        isWaveInProgress = true;

        // 웨이브 시작 시 호출
        if (GameManager.Instance.currentStageType == StageType.NormalBattle)
        {
            waveMessageUI.ShowMessage($"Wave {currentWave}");
        }

        Debug.Log($"[현재 웨이브: {currentWave}]의 몬스터 수: {count} 스폰!");
    }

    //  몬스터 사망 시 호출, 웨이브 종료 조건을 확인하는 메서드
    public void OnEnemyDeath()
    {
        aliveEnemies--;
        Debug.Log($"[OnEnemyDeath] 몬스터 처치됨 - 남은 몬스터: {aliveEnemies}");

        //  웨이브 내 모든 적이 처치될 경우, 다음 웨이브 혹은 던전 클리어 처리
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

    //  던전 클리어 처리 메서드 → 씬 내 플레이어가 직접적으로 먹지 않은 경험치들을 자동 흡수, 플레이어 상태 저장 및 다음 스테이지로 이동
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

        // 마지막 웨이브 끝나면 && 보스가 아닐때만
        if (GameManager.Instance.currentStageType == StageType.NormalBattle)
        {
            waveMessageUI.ShowMessage("CLEAR");
        }
        else
        {
            UIManager.Instance.uiController.clearUI.Show(true);
        }

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