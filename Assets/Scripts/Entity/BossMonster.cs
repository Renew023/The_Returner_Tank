using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : Character
{
	[SerializeField] private Transform expParent;
	[SerializeField] private int exp;
	[SerializeField] private GameObject expPrefab;
	[SerializeField] private Player target;
	[SerializeField] private List<WeaponController> weaponController;
	[SerializeField] private Transform monsterTransform;

    [Header("Sound Effects")]
    public AudioClip damageClip;           // 인스펙터에 할당할 피격 사운드
    public AudioClip DeathClip;          // 인스펙터에 할당할 피격 사운드
    private AudioSource audioSource;       // AudioSource 캐시

    protected Animator animator;

    protected override void Start()
    {
        //효과음 넣기
        audioSource = GetComponent<AudioSource>();

        base.Start();
        target = FindObjectOfType<Player>();
        animator = GetComponentInChildren<Animator>();
        UIManager.Instance.uiController.SetBossHP(true);
        UIManager.Instance.uiController.bossHP.UpdateValue(curHp, maxHp);
    }

    private void Update()
    {
		Move();
		Rotate();
		lookDirection = (target.transform.position - transform.position);
        for (int i = 0; i < weaponController.Count; i++)
        {
            weaponController[i].targetDirect = lookDirection;
        }
		//attackerTimer += Time.deltaTime;

		//if(attackerTimer >= attackInterval)
		//{
		//    attackerTimer = 0.0f;

		//    int attackPattern = Random.Range(0, 3);

		//    switch(attackPattern)
		//    {
		//        case 0: 
		//            BasicAttack();
		//            break;
		//        case 1: ConeAttack();
		//            break;
		//        case 2:
		//            GroundSlam();
		//            break;
		//    }
		//}
	}

    //void BasicAttack()
    //{
    //    Vector2 dir = (target.transform.position - transform.position).normalized;
    //    GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    //    proj.GetComponent<Rigidbody2D>().velocity = dir * 10f; // 예시 속도
    //}

    //void ConeAttack()
    //{
    //    int bulletCount = 5;
    //    float angleStep = 15f;
    //    float angleStart = -((bulletCount - 1) * angleStep) / 2;

    //    for (int i = 0; i < bulletCount; i++)
    //    {
    //        float angle = angleStart + angleStep * i;
    //        Vector2 dir = Quaternion.Euler(0, 0, angle) * (target.transform.position - transform.position).normalized;

    //        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    //        proj.GetComponent<Rigidbody2D>().velocity = dir * 8f;
    //    }
    //}

    void GroundSlam()
    {
        // 여기선 단순히 범위 안에 있는 플레이어에게 피해를 주는 것으로 처리
        float slamRadius = 3.0f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, slamRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Player>().TakeDamage(20); // 예시 데미지
            }
        }

        // 애니메이션이나 이펙트도 여기에서 실행 가능
        Debug.Log("Boss 사용 - 지면 강타!");
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
			if (arrow.owner == this.gameObject)
				return;

			//Hit(ref curHp, arrow.damage);
			TakeDamage(arrow.damage);
			collision.gameObject.SetActive(false);
		}
	}

	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    public void TakeDamage(float damage)
    {
        //효과음
        AudioManager.Instance.PlaySFX(damageClip, 1.0f);


        curHp -= damage;

        animator.SetBool("IsDamage", true);

        //	일정 시간이 지난 후, Damage 애니메이션 플래그 초기화
        StartCoroutine(ResetDamageAnim());

        UIManager.Instance.uiController.bossHP.UpdateValue(curHp, maxHp);

        if (curHp <= 0)
        {
            UIManager.Instance.uiController.SetBossHP(false);
            Death();
        }
    }

    void Death()
    {
        //효과음
        AudioManager.Instance.PlaySFX(DeathClip, 1.0f);

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
