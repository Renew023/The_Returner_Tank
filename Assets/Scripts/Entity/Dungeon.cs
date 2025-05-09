using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    //  TODO: ������Ʈ Ǯ�� ����� Ȱ���Ͽ� ���� ������ ���͵��� ���� ��ȯ�Ѵ�.

    [Header ("Enemies")]
    public GameObject[] Enemies;

    //  Ǯ�� ����ϴ� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        //  pool ����Ʈ�� Enemies �迭�� ũ�⸸ŭ �Ҵ��Ѵ�.
        pools = new List<GameObject>[Enemies.Length];

        //  pool ����Ʈ �� ����ִ� ������ �ʱ�ȭ���ش�.
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject CreateEnemies(int index)
    {
        GameObject selectObject = null;

        //  TODO: ������ Ǯ�� ��� �ִ� ��(��Ȱ��ȭ ��) ������Ʈ�� �����Ѵ�.

        //  ���� �߰��ߴٸ�, select ������Ʈ�� �Ҵ��Ѵ�.
        foreach(GameObject item in pools[index])
        {
            //  ��Ȱ��ȭ�� �� ������Ʈ�� �ִٸ�?
            if(!item.activeSelf)
            {
                selectObject = item;
                selectObject.SetActive(true);
                break;
            }
        }

        //  ���� ��� �ִ� �� ������Ʈ�� ���ٸ�, ���Ӱ� ������ �Ŀ� select ������Ʈ�� �Ҵ��Ѵ�.
        if(!selectObject)
        {
            selectObject = Instantiate(Enemies[index], transform);
            pools[index].Add(selectObject);
        }


        return selectObject;
    }
}
