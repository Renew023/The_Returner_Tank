using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float maxHp = 100;
    protected float curHp;

	protected int arrowValue;
    protected float moveSpeed;
    protected float damage;

    protected float timer = 0.0f;
    protected float attackDelay = 1.0f;
    protected float arrowSpeed;

    protected Vector2 movementDirection;
    protected Vector2 lookDirection;
	protected Vector2 rot;
    protected virtual void Init() 
    {
        curHp = maxHp;
    }

	protected virtual void Move() { }
    protected virtual void Rotate() { }

	protected virtual void Hit(ref float curHp, float damage) 
    {
        curHp -= damage;
    }
}
