using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header ("Spawn Points")]
    public Transform[] spawnPoint;
    public float spawnDelay = 1.0f;

    //  난이도 설정 데이터
    public List<WaveSpawnData> allDifficultyData;

    [Header ("Wave Spawn 설정")]
    public WaveSpawnData[] waveSpawnData;      //  유니티 에디터 상에서 wave 설정

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>()
                     .Where(t => t != this.transform).ToArray();

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            //  제거한다
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

        //  소환할 몬스터 수 = 스폰 포지션의 수만큼 소환 → 4개의 스폰 위치에 소환
        int spawnCount = Mathf.Min(spawnPoint.Length, 4);
        DungeonManager.instance.StartWave(spawnCount);

        //  각 스폰 위치에 해당 몬스터를 소환
        for(int i = 0; i < spawnCount; i++)
        {
            int randomEnemyIndex = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(randomEnemyIndex);

            if(enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position;

                enemy.GetComponent<Enemy>().Initialize();
            }
        }

    }

    private void Start()
    {
        SpawnFixedWave(DungeonManager.instance.currentWave);
    }

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