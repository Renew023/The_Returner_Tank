using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;

    public void Initialize()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //  풀링 시스템용
        gameObject.SetActive(false);
        DungeonManager.instance.OnEnemyDeath();
    }
}
