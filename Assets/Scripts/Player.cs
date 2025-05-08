using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Camera camera;
    [SerializeField] private WeaponController weaponController;

    [SerializeField] private Vector2 mousePosition;
    [SerializeField] private Vector2 worldPos;

    [SerializeField] private Vector2 lookDirection;

    void Update()
    {
		mousePosition = Input.mousePosition;
		worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

		if (Input.GetMouseButton(0))
        {
            weaponController.Attack(lookDirection, 5);
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
}
