using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    [SerializeField] private GameObject warpZone;
    public static DungeonManager instance;

    //  현재 웨이브 변수값
    public int currentWave = 1;
    public int maxWave = 3;

    //  웨이브 내 현재 살아있는 몬스터의 수 변수값
    private int aliveEnemies = 0;

    //  웨이브가 진행 중인지 추적하기 위한 변수
    private bool isWaveInProgress = false;

    public Dungeon pools;           //  오브젝트 풀링 매니저

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
        //  DungeonManager에서 스폰 웨이브를 관리한다!!
        currentWave = 1;

        Spawner.instance.SpawnFixedWave();
        UIManager.Instance.uiController.SetDungeonUI(true);
    }

    //  현재 웨이브 시작 시 몬스터 수 설정 메서드
    public void StartWave(int count)
    {
        aliveEnemies = count;
        isWaveInProgress = true;
        Debug.Log($"[웨이브 {currentWave}] 몬스터 {count} 마리 소환!");
    }

    //  몬스터가 죽었을 때 호출되는 메서드
    public void OnEnemyDeath()
    {
        //  남은 몬스터 수 감소
        aliveEnemies--;

        //  웨이브 내 모든 몬스터가 죽었다면, 현재 웨이브 종료 및 다음 웨이브 스폰
        if(isWaveInProgress && aliveEnemies <= 0)
        {
            isWaveInProgress = false;
            Debug.Log($"웨이브 {currentWave}] 완료!");

            if(currentWave >= maxWave)
            {
                Debug.Log($"[DungeonManager] 모든 웨이브 종료!");
                ClearDungeon();
            }

            else
            {
                Debug.Log($"[DungeonManager] 다음 웨이브 {currentWave}] 시작!");
                //NextWave();

                //  웨이브 증가
                currentWave++;

                Debug.Log($"[DungeonManager] 현재 웨이브: {currentWave}");

                Spawner.instance.SpawnFixedWave();
            }

        }
    }

    void ClearDungeon()
    {
        Debug.Log($"[DungeonManager] 던전 클리어!");

        //  현재 플레이어 HP 저장
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            DataManager.instance.savedPlayerHp = player.CurHP;
            DataManager.instance.savedPlayerMaxHp = player.MaxHp;
        }

        //  던전 난이도 증가
        GameManager.Instance.IncreaseDungeonLevel();

        //  현재 스테이지 인덱스 증가 (다음 스테이지 진행용)
        GameManager.Instance.currentStageIndex++;

        //  다음 던전을 위한 초기화
        currentWave = 1;

        //  스테이지 선택 화면으로 복귀용 워프맵 생성
        warpZone.SetActive(true);
    }
}