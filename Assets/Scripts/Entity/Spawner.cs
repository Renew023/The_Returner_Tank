using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawn Points")]
    public Transform[] spawnPoint;

    //  난이도 설정 데이터
    public List<WaveSpawnData> allDifficultyData;

    [Header ("Wave Spawn 설정")]
    public WaveSpawnData[] waveSpawnData;      //  유니티 에디터 상에서 wave 설정

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

        else
        {
            Destroy(gameObject);
        }
    }

    //  현재 웨이브에 맞는 몬스터들을 무작위로 스폰하는 메서드
    public void SpawnFixedWave(int wave)
    {
        WaveSpawnData data = waveSpawnData.FirstOrDefault(w => w.wave == wave);

        //  GameManager → 현재 스테이지 번호를 불러온다.
        //int stageIndex = GameManager.instance.currentStageIndex;

        //  현재 스테이지에 맞는 난이도 데이터 찾기
        //WaveSpawnData data = allDifficultyData.FirstOrDefault(d => stageIndex >= d.minStageIndex && stageIndex <= d.maxStageIndex);

        if (data == null)
        {
            Debug.LogWarning($"[Spawner] Wave {wave} 설정 없음");
            //Debug.LogWarning($"[Spawner] 해당 스테이지 ({stageIndex})의 난이도 데이터가 존재하지 않습니다");
            return;
        }
    }

    //  몬스터들 스폰 기능
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

    //  Enemies 배열에서 어떤 몬스터를 쓸지 인덱스로 명시
    public int[] enemyIndices;

    //  난이도 적용 시작 스테이지 번호
    public int minStageIndex;

    //  난이도 적용 끝 스테이지 번호
    public int maxStageIndex;
}