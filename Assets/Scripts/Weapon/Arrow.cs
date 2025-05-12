using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject owner;

    public WeaponController countReturn;
    [SerializeField] private Rigidbody2D rb;
    public float damage;
    public Vector2 direction;
    public float time;

    void OnEnable()
    {
	    rb.velocity = direction;

	    // 회전값도 직접 설정
	    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
	    
		StartCoroutine("Shoot");
	}
	void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
	    {
		    gameObject.SetActive(false);
	    }
    }
}
