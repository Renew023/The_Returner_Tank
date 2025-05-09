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

    public GameObject CreateEnemies(int index)
    {
        GameObject selectObject = null;

        //  TODO: 선택한 풀의 놀고 있는 적(비활성화 된) 오브젝트에 접근한다.

        //  만약 발견했다면, select 오브젝트에 할당한다.
        foreach(GameObject item in pools[index])
        {
            //  비활성화된 적 오브젝트가 있다면?
            if(!item.activeSelf)
            {
                selectObject = item;
                selectObject.SetActive(true);
                break;
            }
        }

        //  만약 놀고 있는 적 오브젝트가 없다면, 새롭게 생성한 후에 select 오브젝트에 할당한다.
        if(!selectObject)
        {
            selectObject = Instantiate(Enemies[index], transform);
            pools[index].Add(selectObject);
        }


        return selectObject;
    }
}
