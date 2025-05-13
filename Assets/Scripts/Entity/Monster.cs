using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Character
{
	[SerializeField] private Transform expParent;
	[SerializeField] private int exp;
	[SerializeField] private GameObject expPrefab;
	[SerializeField] private Player target;
	[SerializeField] private WeaponController weaponController;
    [SerializeField] private Image hpBarFill;
    [SerializeField] private Transform monsterTransform;
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
        //isLeft = target.transform.position.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        //transform.localScale = isLeft;
        if (target == null) return;

        bool flip = target.transform.position.x < transform.position.x;
        Vector3 newScale = flip ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        monsterTransform.localScale = newScale;
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Arrow"))
		{
			Arrow arrow = collision.gameObject.GetComponent<Arrow>();
			if (arrow.owner.CompareTag("Monster"))
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

        //	���� �ð��� ���� ��, Damage �ִϸ��̼� �÷��� �ʱ�ȭ
		if(this.gameObject.activeSelf)
		{
            StartCoroutine(ResetDamageAnim());
        }

        hpBarFill.fillAmount = curHp / maxHp;

        if (curHp <= 0)
        {
            Death();
        }
    }

    void Death()
	{
		gameObject.SetActive(false);

		//	�ش� ���Ͱ� �����ִ� ���� �� ����.
		DungeonManager.instance.OnEnemyDeath();
	}

	private IEnumerator ResetDamageAnim()
	{
		yield return new WaitForSeconds(0.2f);
		animator.SetBool("IsDamage", false);
	}
	
	void OnEnable()
	{
		hpBarFill.fillAmount = curHp / maxHp;
		expParent = GameObject.Find("ExpObjects")?.transform;
	}
	
	void OnDisable()
	{
		DropExp();
	}
	
	private void DropExp()
	{
		int remainingExp = exp;
		int dropExp = 1;

		while (remainingExp > 0)
		{
			int dropCount = remainingExp % 10;
			for(int i = 0; i < dropCount; i++) SpawnExpObject(dropExp);
			
			remainingExp /= 10;
			dropExp *= 10;
		}
	}
	
	private void SpawnExpObject(int amount)
	{
		Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
		Vector3 spawnPos = transform.position + (Vector3)randomOffset;

		GameObject obj = Instantiate(expPrefab, spawnPos, Quaternion.identity, expParent);
    
		ExpObject expObj = obj.GetComponent<ExpObject>();
		if (expObj != null)
		{
			expObj.expAmount = amount;
		}
	}
}
