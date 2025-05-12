using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
	[SerializeField] protected Rigidbody2D rb;
    public List<WeaponController> weapons = new List<WeaponController>(5); 
    [SerializeField] private AttackTarget attackTarget;

	[SerializeField] private Camera camera;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 worldPos;
	[SerializeField] private float arrowDelay;
	[SerializeField] private float arrowDamage;

	[SerializeField] private float Exp;
    [SerializeField] private float Level;

	//[SerializeField] public List<Skill> skillList = new List<Skill>(10);
	[SerializeField] public List<Skill> playerSkill = new List<Skill>(5);
    [SerializeField] public GameObject skillSelectUI;
	public Weapon playerWeaponStat;

	void Awake()
    {
        DataManager.instance.Pick();
        Init();
        //LevelUp();
    }

    void Update()
    {
		mousePosition = Input.mousePosition;
		worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = attackTarget.Searching(lookDirection, transform.position);
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].targetDirect = lookDirection;
            weapons[i].playerWeaponStat = playerWeaponStat;
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
        isLeft = worldPos.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        transform.localScale = isLeft;
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

            Hit(ref curHp, arrow.damage);
            collision.gameObject.SetActive(false);
        }
	}
}
