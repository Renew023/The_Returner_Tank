using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Character
{
    #region Monster 객체 변수 선언
    [SerializeField] private Transform expParent;
	[SerializeField] private int exp;
	[SerializeField] private GameObject expPrefab;
	[SerializeField] private Player target;
	[SerializeField] private WeaponController weaponController;
    [SerializeField] private Image hpBarFill;
    [SerializeField] private Transform monsterTransform;

    [Header("Sound Effects")]
    public AudioClip damageClip;           // 인스펙터에 할당할 피격 사운드
    public AudioClip DeathClip;
    private AudioSource audioSource;       // AudioSource 캐시


    protected Animator animator;

	private bool isDead = false;

    #endregion

    #region Awake, Init, Start 메서드
    void Awake()
	{
		//효과음 넣기
        audioSource = GetComponent<AudioSource>();

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

    #endregion

    #region Update 메서드
    void Update()
	{
		Move();

		Rotate();

		lookDirection = (target.transform.position - transform.position);

		weaponController.targetDirect = lookDirection;
	}

    #endregion

    #region Move, Rotate 메서드
    override protected void Move()
	{
		animator.SetBool("IsMove", true);
	}

	override protected void Rotate()
	{
		if (target == null)
		{
			return;
		}

        bool flip = target.transform.position.x < transform.position.x;

        Vector3 newScale = flip ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        monsterTransform.localScale = newScale;
    }

    #endregion

    #region 투사체와 충돌 시를 처리하는 메서드
    public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Arrow"))
		{
			Arrow arrow = collision.gameObject.GetComponent<Arrow>();
			if (arrow.owner.CompareTag("Monster"))
			{
                return;
            }

            TakeDamage(arrow.damage);

			collision.gameObject.SetActive(false);
		}
	}

    #endregion

    #region TakeDamage 메서드 → 데미지 처리 기능
    public void TakeDamage(float damage)
    {
        //효과음
        if (damageClip != null)
		{
            var listenerPos = Camera.main.transform.position;

            AudioSource.PlayClipAtPoint(damageClip, transform.position,1.0f);
        }

        curHp -= damage;

        animator.SetBool("IsDamage", true);

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

    #endregion

    #region Death 메서드 → 몬스터 사망 처리 기능
    void Death()
	{
        //효과음
        if (DeathClip != null)
		{
            var listenerPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(DeathClip, transform.position,1.0f);
        }

        //	중복 호출을 방지!
        if (isDead)
		{
			return;
		}

		isDead = true;

		gameObject.SetActive(false);

		DungeonManager.instance.OnEnemyDeath();
	}

    #endregion

    #region ResetDamageAnim 메서드 → 피격 애니메이션 실행 후, 지정한 텀이 지난 후 피격 애니메이션을 종료하는 기능
    private IEnumerator ResetDamageAnim()
	{
		yield return new WaitForSeconds(0.2f);
		animator.SetBool("IsDamage", false);
	}

    #endregion

    #region OnEnable, OnDisable 메서드
    void OnEnable()
	{
		hpBarFill.fillAmount = curHp / maxHp;
		expParent = GameObject.Find("ExpObjects")?.transform;
	}
	
	void OnDisable()
	{
		DropExp();
	}

    #endregion

    #region DropExp 메서드
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

    #endregion

    #region SpawnExpObject 메서드
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

    #endregion

    #region ResetEnemy 메서드 → 몬스터 재스폰 시, 초기화할 데이터를 관리하는 기능
    public void ResetEnemy()
	{
        curHp = maxHp;

		//	초기화
		isDead = false;

        Debug.Log($"[ResetEnemy] {name}, curHp: {curHp}");

		if(hpBarFill != null)
		{
			hpBarFill.fillAmount = 1f;
		}

		if(animator != null)
		{
			//	색상 값 초기화

			SpriteRenderer spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();

			spriteRenderer.color = Color.white;

			animator.SetBool("IsDamage", false);
			animator.SetBool("IsMove", false);
		}

		gameObject.SetActive(true);
	}

    #endregion
}