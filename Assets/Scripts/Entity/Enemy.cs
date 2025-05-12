using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;

    public void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bomb")
        {
            Debug.Log("트리거 충돌 발생!");

            TakeDamage(10);
        }
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
