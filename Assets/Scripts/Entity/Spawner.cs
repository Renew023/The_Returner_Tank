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

    [Header ("난이도 설정")]
    public WaveSpawnData[] waveSpawnData;      //  유니티 에디터 상에서 wave 설정

    public int testWave = 1;
    public bool useTestWave = false;

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
    public void SpawnFixedWave()
    {
        int wave = useTestWave ? testWave : DungeonManager.instance.currentWave;

        //  현재 설정된 던전 레벨 가져오기
        int dungeonLevel = GameManager.Instance.dungeonLevel;

        //  난이도 정보 가져오기
        WaveSpawnData data = waveSpawnData
        .FirstOrDefault(d => d.wave == wave && dungeonLevel >= d.minStageIndex && dungeonLevel <= d.maxStageIndex);

        if (data == null)
        {
            Debug.LogWarning($"[Spawner] 던전 레벨 ({dungeonLevel})에 맞는 난이도 정보가 없습니다");
            return;
        }

        //  소환할 몬스터 수 = Inspector 창에서 지정한 enemyIndices의 수만큼 지정된 스폰 위치에서 스폰한다.
        int spawnCount = Mathf.Min(spawnPoint.Length, data.enemyIndices.Length);

        DungeonManager.instance.StartWave(spawnCount);

        //  각 스폰 위치에 해당 몬스터를 소환
        for(int i = 0; i < spawnCount; i++)
        {
            int randomnemyIndex = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(randomnemyIndex);

            if(enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position;
            }
        }

        if(useTestWave)
        {
            DungeonManager.instance.currentWave = testWave;
        }

    }

    private void Start()
    {
        SpawnFixedWave();
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