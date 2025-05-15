using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Arrow
{
    #region Start, OnEnable, OnDisable 메서드
    void Start()
    {
    }

    void OnEnable()
    {
		StartCoroutine("Beam");
	}
	void OnDisable()
	{
        StopAllCoroutines();
		countReturn.pooling();
	}

    #endregion

    #region Update 메서드
    // Update is called once per frame
    void Update()
    {
		Action();
	}

    #endregion

    #region Action, Shoot 메서드 → 투사체 발사 시, 날아가는 속도 기능 / 발사 시 시간 관리 기능
    public override void Action()
	{
		rb.velocity = direction;
	}

	public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    #endregion
}