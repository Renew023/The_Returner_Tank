using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//  몬스터 스폰 기능 객체
public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header ("Spawn Points")]
    //  몬스터가 소환될 위치 배열
    public Transform[] spawnPoint;

    [Header ("Wave 설정")]
    //  웨이브 별 몬스터 데이터 리스트
    public List<WaveSpawnEntry> waveSpawnEntries;

    //  딕셔너리 자료형을 활용한 레벨에 따른 WaveSpawnData 지정 변수.
    private Dictionary<(int wave, int dungeonLevel), WaveSpawnData> spawnDataDict;

    //  Unity - Editor 상에서 웨이브 테스트용 변수들
    public int testWave = 1;
    public bool useTestWave = false;

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

        spawnPoint = GetComponentsInChildren<Transform>().Where(t => t != this.transform).ToArray();

        BuildSpawnDataDictionary();
    }

    //  웨이브 데이터 딕셔너리를 구성시켜주는 메서드
    private void BuildSpawnDataDictionary()
    {
        spawnDataDict = new Dictionary<(int wave, int dungeonLevel), WaveSpawnData>();

        foreach (var entry in waveSpawnEntries)
        {
            var key = (entry.wave, entry.dungeonLevel);

            if (!spawnDataDict.ContainsKey(key))
            {
                spawnDataDict.Add(key, entry.data);
            }

            else
            {
                //  [예외 처리] 중복 키가 있을 경우, 경고 메시지 출력 → 중복 데이터는 무시한다.
                Debug.LogWarning($"[Spawner] 중복된 Key : wave {key.wave}, level {key.dungeonLevel}");
            }
        }
    }

    //  현재 웨이브에 맞는 몬스터들을 무작위로 스폰하는 메서드
    public void SpawnFixedWave()
    {
        // [예외 처리] 웨이브 시작 전에 'aliveEnemies'가 0이 아니면 더 이상 스폰하지 않음
        if (DungeonManager.instance.GetAliveEnemies() > 0)
        {
            Debug.LogWarning("웨이브가 끝나지 않았습니다. 적들이 모두 사라질 때까지 기다려주세요.");
            return;
        }

        //  Unity - Editor 상에서 웨이브를 테스트하고 싶다면, 테스트할 웨이브 값을 결정
        int wave = useTestWave ? testWave : DungeonManager.instance.currentWave;

        int dungeonLevel = GameManager.Instance.dungeonLevel;

        //  [예외 처리] 해당 웨이브, 던전 레벨 조합에 맞는 데이터가 없으면 경고 메시지 출력
        if (!spawnDataDict.TryGetValue((wave, dungeonLevel), out var data))
        {
            Debug.LogWarning($"[Spawner] wave {wave}, level {dungeonLevel}에 맞는 데이터가 없습니다");
            return;
        }

        int spawnCount = Mathf.Min(spawnPoint.Length, data.baseEnemyCount + dungeonLevel);

        //  실제로 스폰되는 적들의 수 → 해당 변수를 사용하여 몬스터들을 생성한다.
        int realSpawned = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            int index = data.enemyIndices[Random.Range(0, data.enemyIndices.Length)];

            GameObject enemy = DungeonManager.instance.pools.CreateEnemies(index);

            if (enemy != null)
            {
                enemy.transform.position = spawnPoint[i].position;
                realSpawned++;
            }
        }

        //  성공적으로 생성되는 적들만 설정하여 소환
        DungeonManager.instance.StartWave(realSpawned);

        if (useTestWave)
        {
            DungeonManager.instance.currentWave = testWave;
        }
    }
}

//  웨이브 별 소환 정보 클래스
[System.Serializable]
public class WaveSpawnData
{
    public int[] enemyIndices;
    public int baseEnemyCount;
}

//  웨이브와 던전 레벨 조합으로 구성하는 웨이브 엔트리 클래스
[System.Serializable]
public class WaveSpawnEntry
{
    public int wave;
    public int dungeonLevel;
    public WaveSpawnData data;
}