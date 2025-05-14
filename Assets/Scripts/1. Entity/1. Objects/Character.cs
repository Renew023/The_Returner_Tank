using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region 해당 객체가 가지고 있어야 할 내용 주석 
    /*
     * 플레이어가 가지고 있어야되는 무기값 : 딜레이
     * 무기에 전달해야되는 값 : 발사체 개수, 발사할 위치, 발사 속도, 데미지
     * 발사체가 가지고 있어야하는 값 : 데미지, 
     */
    #endregion

    #region Character 객체 변수 선언
    [SerializeField] protected float maxHp;
	[SerializeField] protected float curHp;
	[SerializeField] protected float moveSpeed;

	protected Vector2 isLeft;

    [SerializeField] protected Vector2 lookDirection;

    [SerializeField] protected float attackDelay = 1.0f;

    protected float timer = 0.0f;

    #endregion

    #region 외부에서 참조하기 위한 Get CurHP, MaxHP 메서드 
    //	외부에서 읽고 쓰기 위한 함수 추가
    public float CurHP
	{
		get => curHp;
	}

	public float MaxHp
	{
		get => maxHp;
	}

    #endregion

    #region Init, Start, Move, Rotate 메서드 → 자식 객체에게 상속시켜줄 메서드들
    protected virtual void Init() 
    {
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

    #endregion
}
