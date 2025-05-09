using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    //  현재 웨이브 변수값
    public int currentWave = 1;

    //  웨이브 내 현재 살아있는 몬스터의 수 변수값
    private int aliveEnemies = 0;

    //  웨이브가 진행 중인지 추적하기 위한 변수
    private bool isWaveInProgress = false;

    public Dungeon pools;           //  오브젝트 풀링 매니저

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
            Debug.Log($"[웨이브 {currentWave}] 완료!");
            NextWave();
        }
    }

    //  다음 웨이브 호출 메서드
    void NextWave()
    {
        //  웨이브 증가
        currentWave++;

        Debug.Log($"[DungeonManager] 현재 웨이브: {currentWave}");

        Spawner.instance.SpawnFixedWave(currentWave);
    }

    private void Update()
    {
        //  웨이브 증가
        //currentWave++;

        //Debug.Log($"[DungeonManager] 현재 웨이브: {currentWave}");
    }
}