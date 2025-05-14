using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    #region Dungeon 객체 변수 선언
    [Header ("Enemies")]
    public GameObject[] Enemies;

    //  풀을 담당하는 리스트들
    List<GameObject>[] pools;

    #endregion

    #region Awake 메서드
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

    #endregion

    #region CreateEnemies 메서드 → 몬스터 생성 메서드
    //  오브젝트 풀링을 활용한 몬스터 생성 메서드
    public GameObject CreateEnemies(int index)
    {
        if (index < 0 || index >= Enemies.Length)
        {
            Debug.LogError($"[CreateEnemies] 잘못된 인덱스 접근: {index}");
            return null;
        }

        GameObject selectObject = null;

        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                selectObject = obj;
                break;
            }
        }

        if (!selectObject)
        {
            selectObject = Instantiate(Enemies[index], transform);
            pools[index].Add(selectObject);
        }

        // 초기화
        Monster enemyComponent = selectObject.GetComponent<Monster>();

        if (enemyComponent != null)
        {
            enemyComponent.ResetEnemy();
        }

        else
        {
            // 기본 활성화만 처리 (예외적으로)
            selectObject.SetActive(true);
        }

        return selectObject;
    }

    #endregion
}
