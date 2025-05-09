using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	/*
     * �÷��̾ ������ �־�ߵǴ� ���Ⱚ : ������
     * ���⿡ �����ؾߵǴ� �� : �߻�ü ����, �߻��� ��ġ, �߻� �ӵ�, ������
     * �߻�ü�� ������ �־���ϴ� �� : ������, 
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
