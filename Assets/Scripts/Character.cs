using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] protected Rigidbody2D rb;

    protected float Hp;

	protected int arrowValue;
    protected float moveSpeed;
    protected float damage;

    protected float timer = 0.0f;
    protected float attackDelay = 1.0f;
    protected float arrowSpeed;

    protected Vector2 movementDirection;
    protected Vector2 LookDirection;
	protected Vector2 rot;
    protected virtual void Init() { }

	protected virtual void Move() { }
    protected virtual void Rotate() { }

	protected virtual void Attack() { }
}
