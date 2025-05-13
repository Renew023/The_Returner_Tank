using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Character
{
	[SerializeField] protected Rigidbody2D rb;
    [SerializeField] private AttackTarget attackTarget;

	[SerializeField] private Camera camera;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 worldPos;

    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform headTransform;

    public PlayerValue playerValue;

	//[SerializeField] public List<Skill> skillList = new List<Skill>(10);
	
    [SerializeField] public GameObject skillSelectUI;

    protected Animator animator;

    void Awake()
    {
        rb.freezeRotation = true;
        animator = GetComponentInChildren<Animator>();
        
        //LevelUp();
    }

    void OnEnable()
    {
        DataManager.instance.Pick();

		for (int i = 0; i < DataManager.instance.playerValue.playerSkill.Count; i++)
        {
            playerValue.playerSkill.Add(DataManager.instance.playerValue.playerSkill[i]);

            if (playerValue.playerSkill[i].weaponCon != null)
            {
                playerValue.weapons.Add(Instantiate(DataManager.instance.playerValue.playerSkill[i].weaponCon, transform.position, Quaternion.identity, transform));
            }
        }

        for (int j = 0; j < playerValue.weapons.Count; j++)
        {
            playerValue.weapons[j].weapon = DataManager.instance.playerWeapon[j];
        }
        playerValue.playerWeaponStat = DataManager.instance.playerValue.playerWeaponStat;
        playerValue.Exp = DataManager.instance.playerValue.Exp;
		playerValue.Level = DataManager.instance.playerValue.Level;

		DataManager.instance.Pick();
		//DataManager.instance.player = gameObject.transform.GetComponent<Player>();
	}

    void OnDisable()
	{
        DataManager.instance.playerValue = playerValue;

        for (int i = 0; i < DataManager.instance.playerWeapon.Count; i++)
        {
            DataManager.instance.playerWeapon[i] = playerValue.weapons[i].weapon;
        }

        for (int j = DataManager.instance.playerWeapon.Count; j < playerValue.weapons.Count; j++)
        {
			DataManager.instance.playerWeapon.Add(playerValue.weapons[j].weapon);
		}
	}

    private void Start()
    {
        base.Start();
		
		if (DataManager.instance.savedPlayerMaxHp > 0)
        {
            maxHp = DataManager.instance.savedPlayerMaxHp;
            curHp = DataManager.instance.savedPlayerHp;
        }

    }

    void Update()
    {
		mousePosition = Input.mousePosition;
		worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = attackTarget.Searching(lookDirection, transform.position);
        RotateHead();
        
        for (int i = 0; i < playerValue.weapons.Count; i++)
        {
            playerValue.weapons[i].targetDirect = lookDirection;
            playerValue.weapons[i].playerWeaponStat = playerValue.playerWeaponStat;
        }
		//lookDirection 은 가장 가까운 몬스터를 타겟팅 해야함.
		//lookDirection = (worldPos - (Vector2)transform.position);

		/*if (Input.GetMouseButton(0) && (timer > attackDelay))
        {
            weaponController.Attack(lookDirection);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }*/

        Move();
        Rotate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelUp();
        }
    }

    override protected void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;
        rb.velocity = move * 10f;
    }

    override protected void Rotate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            bodyTransform.rotation = Quaternion.Euler(0, 0, angle + 90f); // ??? +90 ??
        }
    }
    
    private void RotateHead()
    {
        if (lookDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0, 0, angle - 100f);
        }
    }

    public void HpUp(float value = 1)
    {
		maxHp += value;
		curHp += value;
	}

    public void MoveSpeedUp(float value = 1)
    {
        moveSpeed += value;
    }


	private void LevelUp()
    {
        HpUp(50);
        Time.timeScale = 0.0f;
        skillSelectUI.gameObject.SetActive(true);
        //배틀매니저에서 게임 멈췄는지 관리
        //레벨업 선택창 UI

        if (curHp + maxHp * .1f < maxHp)
        {
            curHp += maxHp * .1f;
        }
        else
        {
            curHp = maxHp;
        }
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

        //animator.SetBool("IsDamage", true);

        //	일정 시간이 지난 후, Damage 애니메이션 플래그 초기화
        //StartCoroutine(ResetDamageAnim());

        if (curHp <= 0)
        {
            //Death();
        }
    }


    private IEnumerator ResetDamageAnim()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsDamage", false);
    }
}
