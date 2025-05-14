using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    //  TODO: 오브젝트 풀링 방식을 활용하여 스폰 지역에 몬스터들을 랜덤 소환한다.

    [Header ("Enemies")]
    public GameObject[] Enemies;

    //  풀을 담당하는 리스트들
    List<GameObject>[] pools;

    private void Awake()
    {
        //  pool 리스트를 Enemies 배열의 크기만큼 할당한다.
        pools = new List<GameObject>[Enemies.Length];

        //  pool 리스트 내 들어있는 적들을 초기화해준다.
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    //  오브젝트 풀링을 활용한 몬스터 생성 메서드
    public GameObject CreateEnemies(int index)
    {
        //  [예외 처리] 인덱스가 범위를 벗어날 경우, 에러 로그 출력 후 null 값 반환
        if (index < 0 || index >= Enemies.Length)
        {
            Debug.LogError($"[CreateEnemies] 잘못된 인덱스 접근: {index}");
            return null;
        }

        GameObject selectObject = null;

        //  오브젝트 풀에서 비활성화된 몬스터 오브젝트가 있는지 탐색
        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                selectObject = obj;
                break;
            }
        }

        //  재사용 가능한 오브젝트가 없을 경우, 새로 인스턴스화하여 풀에 추가
        if (!selectObject)
        {
            selectObject = Instantiate(Enemies[index], transform);
            pools[index].Add(selectObject);
            Debug.Log("[Dungeon CreateEnemies] 몬스터 스폰 성공");
        }

        // 몬스터 초기화 진행!
        Monster enemyComponent = selectObject.GetComponent<Monster>();

        if (enemyComponent != null)
        {
            //  몬스터 상태 초기화 
            enemyComponent.ResetEnemy();
        }

        else
        {
            // 예외적으로 Monster 컴포넌트가 없을 경우, 기본적으로 활성화만 진행시킨다.
            Debug.Log($"[Dungeon CreateEnemies enemyComponent가 null값일 때]");
            selectObject.SetActive(true);
        }

        return selectObject;
    }
}