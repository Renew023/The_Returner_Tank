using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
	[SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Arrow example_arrow;
    [SerializeField] private AttackTarget attackTarget;

	[SerializeField] private Camera camera;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 worldPos;

    void Awake()
    {
        Init();
    }

    void Update()
    {
		mousePosition = Input.mousePosition;
		worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = attackTarget.Searching() - (Vector2)transform.position;
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
		if ((timer > attackDelay))
		{
			weaponController.Attack(lookDirection);
			timer = 0f;
		}
		else
		{
			timer += Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.R))
        {
            
        }

        Move();
        Rotate();
    }

    void WeaponAdd()
    {

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

    void LevelUp()
    {
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
