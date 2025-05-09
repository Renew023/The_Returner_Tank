using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	/*
     * 플레이어가 가지고 있어야되는 무기값 : 딜레이
     * 무기에 전달해야되는 값 : 발사체 개수, 발사할 위치, 발사 속도, 데미지
     * 발사체가 가지고 있어야하는 값 : 데미지, 
     * 
     */
	[SerializeField] protected WeaponController weaponController;

	[SerializeField] protected float maxHp = 100;
	[SerializeField] protected float curHp;
	[SerializeField] protected float moveSpeed;
	protected Vector2 isLeft;

	[SerializeField] protected int arrowValue;
	[SerializeField] protected float arrowSpeed;
	[SerializeField] protected float arrowDamage;
	protected Vector2 lookDirection;

	[SerializeField] protected float attackDelay = 1.0f;
	protected float timer = 0.0f;

	protected virtual void Init() 
    {
		maxHp = 100;
		curHp = maxHp;
		moveSpeed = 10;
		isLeft = Vector3.zero;

		arrowValue = 1;
		arrowSpeed = 10;
		arrowDamage = 10;
		lookDirection = Vector3.zero;

		timer = 0.0f;
		attackDelay = 1.0f;
    }

	protected virtual void Move() { }
    protected virtual void Rotate() { }

	protected virtual void Hit(ref float curHp, float damage) 
    {
        curHp -= damage;
    }
}
