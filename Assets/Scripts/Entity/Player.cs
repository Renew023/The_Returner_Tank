using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertial = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, vertial).normalized;

        //  오직 키보드를 통해서만 처리할 예정이다.
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            lookDirection = new Vector2(horizontal, 0).normalized;
        }
    }
}
