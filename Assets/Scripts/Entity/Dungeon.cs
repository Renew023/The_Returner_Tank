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

    //  ������Ʈ Ǯ���� Ȱ���� ���� ���� �޼���
    public GameObject CreateEnemies(int index)
    {
        //  ���� ó��
        if(index < 0 || index >= Enemies.Length)
        {
            Debug.LogError($"[CreateEnemies] �߸��� �ε��� ����: {index}");
            return null;
        }

        GameObject selectObject = null;

        //  ���� �߰��ߴٸ�, select ������Ʈ�� �Ҵ��Ѵ�.
        foreach(GameObject obj in pools[index])
        {
            //  ��Ȱ��ȭ�� �� ������Ʈ�� �ִٸ�?
            if(!obj.activeSelf)
            {
                selectObject = obj;
                selectObject.SetActive(true);
                break;
            }
        }

        //  ���� ��� �ִ� �� ������Ʈ�� ���ٸ�, ���Ӱ� ������ �Ŀ� select ������Ʈ�� �Ҵ��Ѵ�.
        if(!selectObject)
        {
            Debug.Log(index);
            selectObject = Instantiate(Enemies[index], transform);
            pools[index].Add(selectObject);
        }

        return selectObject;
    }
}
