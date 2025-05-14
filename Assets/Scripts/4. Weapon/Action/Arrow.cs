using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Arrow 객체 변수 선언
    public GameObject owner;
    public WeaponController countReturn;
    public Rigidbody2D rb;
    public float damage;
    public Vector2 direction;
    public float time;

    #endregion

    #region OnEnable, OnDisable 메서드
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

    #endregion

    #region Update 메서드
    public virtual void Update()
    {
        Action();
	}

    #endregion

    #region Action, Shoot 메서드 → 투사체 발사 시, 날아가는 속도 기능 / 발사 시 시간 관리 기능
    public virtual void Action()
    {
		rb.velocity = direction;
	}

    public virtual IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    #endregion

    #region 투사체와 "벽" 충돌 처리 메서드
    public void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
	    {
		    gameObject.SetActive(false);
	    }
    }

    #endregion
}
