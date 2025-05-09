using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawn Points")]
    public Transform[] spawnPoint;

    float timer = 0.0f;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1.0f)
        {
            timer = 0.0f;
            SpawnEnemis();
        }
    }

    //  몬스터들 스폰 기능
    void SpawnEnemis()
    {
        GameObject enemy = DungeonManager.instance.pools.CreateEnemies(Random.Range(0, 4));

        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        //enemy.transform.position = new Vector2(3,3);
    }
}