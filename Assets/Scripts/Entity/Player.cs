using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
	[SerializeField] protected Rigidbody2D rb;

	[SerializeField] private Camera camera;
    [SerializeField] private WeaponController weaponController;

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
        lookDirection = (worldPos - (Vector2)transform.position);

        if (Input.GetMouseButton(0) && timer > attackDelay)
        {
            weaponController.Attack(lookDirection, 5);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
        Move();
        Rotate();
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
        rot = worldPos.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        transform.localScale = rot;
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
