using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject owner;
    public WeaponController countReturn;
    public Rigidbody2D rb;
    public float damage;
    public Vector2 direction;
    public float time;

    protected virtual void OnEnable()
    {
	    rb.velocity = direction;

	    // 회전값도 직접 설정
	    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
	    
		StartCoroutine("Shoot");
	}
	protected virtual void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}
	
	// Update is called once per frame
	public virtual void Update()
    {
        Action();
	}

    public virtual void Action()
    {
		rb.velocity = direction;
	}

    public virtual IEnumerator Shoot()
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
