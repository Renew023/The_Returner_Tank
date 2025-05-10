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
            Debug.Log("Ʈ���� �浹 �߻�!");

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
        //  Ǯ�� �ý��ۿ�
        gameObject.SetActive(false);
        DungeonManager.instance.OnEnemyDeath();
    }
}
