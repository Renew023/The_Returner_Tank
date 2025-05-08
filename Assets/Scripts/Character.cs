using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] protected Rigidbody2D rb;

	protected int arrow;
    protected float moveSpeed;
    protected float damage;
    protected float attackDelay;
    protected float arrowSpeed;

    protected Vector2 movementDirection;
    protected Vector2 LookDirection;
	protected Vector2 rot;

	protected virtual void Move() { }
    protected virtual void Rotate() { }

	protected virtual void Attack() { }
}
