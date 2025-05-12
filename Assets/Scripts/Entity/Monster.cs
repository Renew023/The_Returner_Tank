using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
	[SerializeField] private Player target;
	[SerializeField] private WeaponController weaponController;
	protected Animator animator;

	void Awake()
	{
		target = GameObject.FindObjectOfType<Player>();

		animator = GetComponentInChildren<Animator>();
	}

	protected override void Init()
	{
		base.Init();
	}

	protected override void Start()
	{
		base.Start();
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
		animator.SetBool("IsMove", true);
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

		animator.SetBool("IsDamage", true);

		//	일정 시간이 지난 후, Damage 애니메이션 플래그 초기화
		StartCoroutine(ResetDamageAnim());

		if (curHp <= 0)
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

	private IEnumerator ResetDamageAnim()
	{
		yield return new WaitForSeconds(0.2f);
		animator.SetBool("IsDamage", false);
	}
}
