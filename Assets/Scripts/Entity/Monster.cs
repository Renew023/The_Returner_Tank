using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	[SerializeField] private Player target;
	[SerializeField] private WeaponController weaponController;

	void Awake()
	{
		target = GameObject.FindObjectOfType<Player>();
	}

    protected override void Init()
    {
        base.Init();
    }

    void Update()
	{
		Move();
		Rotate();
		lookDirection = (target.transform.position - transform.position);
		weaponController.targetDirect = lookDirection;
		//if ((timer > attackDelay))
		//{
		//	weaponController.Attack(lookDirection);
		//	timer = 0f;
		//}
		//else
		//{
		//	timer += Time.deltaTime;
		//}
		Move();
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

			//Hit(ref curHp, arrow.damage);
			TakeDamage(arrow.damage);
			collision.gameObject.SetActive(false);
		}
	}

	public void TakeDamage(float damage)
	{
		curHp -= damage;

		if(curHp <= 0)
		{
            Death();
		}
	}

	void Death()
	{
		gameObject.SetActive(false);

		//	해당 몬스터가 속해있는 몬스터 수 감소.
		DungeonManager.instance.OnEnemyDeath();
	}
}
