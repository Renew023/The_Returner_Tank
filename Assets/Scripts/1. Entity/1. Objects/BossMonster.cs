using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : Character
{
    #region BossMonster 변수 선언
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

    #endregion

    #region Start 메서드
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

    #endregion

    #region Update 메서드
    private void Update()
    {
		Move();

		Rotate();

		lookDirection = (target.transform.position - transform.position);

        for (int i = 0; i < weaponController.Count; i++)
        {
            weaponController[i].targetDirect = lookDirection;
        }
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

    #region 투사체와 충돌할 때의 처리 메서드
    public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Arrow"))
		{
			Arrow arrow = collision.gameObject.GetComponent<Arrow>();

			if (arrow.owner == this.gameObject)
            {
                return;
            }

            TakeDamage(arrow.damage);

			collision.gameObject.SetActive(false);
		}
	}

    #endregion

    #region OnDrawGizmosSelected 메서드
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    #endregion

    #region TakeDamage 메서드 → 데미지 처리 메서드
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

    #endregion

    #region Death 메서드 → 사망 처리 메서드
    void Death()
    {
        //효과음
        AudioManager.Instance.PlaySFX(DeathClip, 1.0f);

        gameObject.SetActive(false);

        //	해당 몬스터가 속해있는 몬스터 수 감소.
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
}
