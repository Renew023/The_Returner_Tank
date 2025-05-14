using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	/*
     * 플레이어가 가지고 있어야되는 무기값 : 딜레이
     * 무기에 전달해야되는 값 : 발사체 개수, 발사할 위치, 발사 속도, 데미지
     * 발사체가 가지고 있어야하는 값 : 데미지, 
     */

	[SerializeField] protected float maxHp;
	[SerializeField] protected float curHp;
	[SerializeField] protected float moveSpeed;
	protected Vector2 isLeft;

	//	외부에서 읽고 쓰기 위한 함수 추가
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
