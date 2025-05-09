using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	[SerializeField] private Player target;

	void Update()
	{
		Move();
		Rotate();
		lookDirection = (target.transform.position - transform.position);
		if (timer > attackDelay)
		{
			weaponController.Attack(arrowValue, lookDirection, arrowSpeed, arrowDamage);
			timer = 0f;
		}
		else
		{
			timer += Time.deltaTime;
		}
	}

	override protected void Move()
	{

	}

	override protected void Rotate()
	{
		isLeft = target.transform.position.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

		transform.localScale = isLeft;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Arrow"))
		{
			Arrow arrow = collision.gameObject.GetComponent<Arrow>();
			if (arrow.owner == this.gameObject)
				return;

			Hit(ref curHp, arrow.damage);
			collision.gameObject.SetActive(false);
		}
	}
}
