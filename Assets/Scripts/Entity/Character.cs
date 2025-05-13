using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	/*
     * �÷��̾ ������ �־�ߵǴ� ���Ⱚ : ������
     * ���⿡ �����ؾߵǴ� �� : �߻�ü ����, �߻��� ��ġ, �߻� �ӵ�, ������
     * �߻�ü�� ������ �־���ϴ� �� : ������, 
     */

	[SerializeField] protected float maxHp;
	[SerializeField] protected float curHp;
	[SerializeField] protected float moveSpeed;
	protected Vector2 isLeft;

	//	�ܺο��� �а� ���� ���� �Լ� �߰�
	public float CurHP
	{
		get => curHp;
	}

	public float MaxHp
	{
		get => maxHp;
	}

	//[SerializeField] protected int arrowValue;
	//[SerializeField] protected float arrowSpeed;
	//[SerializeField] protected float arrowDamage;
	[SerializeField] protected Vector2 lookDirection;

	[SerializeField] protected float attackDelay = 1.0f;
	protected float timer = 0.0f;

	protected virtual void Init() 
    {
		//maxHp = 100;
		curHp = maxHp;
		//moveSpeed = 10;
		isLeft = Vector3.zero;

		lookDirection = Vector3.up;
    }

    protected virtual void Start()
    {
		Init();
    }

    protected virtual void Move() { }
    protected virtual void Rotate() { }

	protected virtual void Hit(ref float curHp, float damage) 
    {
        curHp -= damage;
	}
}
