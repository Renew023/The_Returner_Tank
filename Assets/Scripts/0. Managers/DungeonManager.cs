using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    #region DungeonManager 변수 선언

    public static DungeonManager instance;

    [SerializeField] private Transform expParent; // ExpObjects 오브젝트
    [SerializeField] private Transform player;    // 플레이어 Transform

    [SerializeField] private GameObject warpZone;
    [SerializeField] private WaveMessageUI waveMessageUI;

    public int currentWave = 1;
    public int maxWave;

    private int aliveEnemies = 0;

    private bool isWaveInProgress = false;

    public Dungeon pools;

    #endregion

    #region OnEnable, OnDisable 메서드
    private void OnEnable()
    {
        UIManager.Instance.uiController.SetDungeonUI(true);
    }

    private void OnDisable()
    {
        UIManager.Instance.uiController.SetDungeonUI(false);
    }

    #endregion

    #region GetAliveEnemies 메서드 → aliveEnemies 변수를 외부에서 가져오게 하기 위함
    public int GetAliveEnemies()
    {
        return aliveEnemies;
    }

    #endregion

    #region Awake 메서드
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

    #endregion

    #region Start 메서드
    private void Start()
    {
        //  DungeonManager���� ���� ���̺긦 �����Ѵ�!!
        currentWave = 1;
        string sceneName = SceneManager.GetActiveScene().name;

        Spawner.instance.SpawnFixedWave();
    }

    #endregion

    #region StartWave 메서드 → 매 웨이브를 시작하는 기능
    public void StartWave(int count)
    {
        aliveEnemies = count;
        isWaveInProgress = true;

        // 웨이브 시작 시 호출
        if (GameManager.Instance.currentStageType == StageType.NormalBattle)
        {
            waveMessageUI.ShowMessage($"Wave {currentWave}");
        }
    }

    #endregion

    #region OnEnemyDeath 메서드 → 웨이브 내 몬스터들이 죽었을 때를 처리하는 기능
    public void OnEnemyDeath()
    {
        aliveEnemies--;

        // 웨이브 진행 중이고, 살아있는 적이 없으면 웨이브 종료
        if (isWaveInProgress && aliveEnemies <= 0)
        {
            isWaveInProgress = false;

            if (currentWave >= maxWave)
            {
                AbsorbExp();
                ClearDungeon();
            }

            else
            {
                // 다음 웨이브로 이동하기 전에 증가시킴
                currentWave++; 
                Spawner.instance.SpawnFixedWave();
            }
        }
    }

    #endregion

    #region ClearDungeon 메서드 → 각 던전 씬 내 모든 웨이브를 클리어할 때를 처리하는 기능
    void ClearDungeon()
    {
        // 플레이어 HP 저장
        Player player = FindObjectOfType<Player>();

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

    #endregion

    #region AbsorbExp 메서드 → 몬스터가 죽었을 때 드랍되는 경험치를 관리하는 기능
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

    #endregion
}