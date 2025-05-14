using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region WeaponController 객체 변수 선언
    public SpriteRenderer weaponSprite;
	public Weapon weapon;
	[SerializeField] private List<Arrow> objectPoolArrow;
	[SerializeField] private int objectPoolCount = 0;
	[SerializeField] private int curCount = 0;
	[SerializeField] public Vector2 targetDirect;
	public Weapon playerWeaponStat;

	public Arrow arrow;

    #endregion

    #region Awake 메서드
    void Awake()
	{
	}

    #endregion

    #region LateUpdate 메서드 
    void LateUpdate()
	{
		if (weapon.timer > (weapon.attackDelay + (weapon.attackDelay * playerWeaponStat.attackDelay)))
		{
			Attack(targetDirect);
			weapon.timer = 0f;
		}
		else
		{
			weapon.timer += Time.deltaTime;
		}
	}

    #endregion

    #region Attack 메서드 → 지정된 방향에 대해 여러 개의 화살을 발사하며, 화살의 속도, 회전, 피해 등을 설정하는 기능
    public void Attack(Vector2 dir)
	{
		int value = weapon.arrowValue + playerWeaponStat.arrowValue;
		float minAngle = -(value / 2f) * 10;

		for (int i = 0; i < value; i++)
		{
			float angleOffset = minAngle + 10 * i;
			Vector2 rozLine = (Quaternion.Euler(0, 0, angleOffset) * dir).normalized;

			float rotZ = Mathf.Atan2(rozLine.y, rozLine.x) * Mathf.Rad2Deg;
			Quaternion arrowRotation = Quaternion.Euler(0, 0, rotZ + 90f);

			if (objectPoolCount < 5)
			{
				arrow.countReturn = this;
				arrow.owner = transform.parent.gameObject;
				arrow.transform.position = transform.position;
				arrow.direction = rozLine * weapon.arrowSpeed;
				arrow.damage = weapon.arrowDamage;

				objectPoolArrow.Add(Instantiate(arrow, transform.position, Quaternion.identity));

				curCount++;
				if (curCount >= objectPoolArrow.Count)
				{
					curCount -= objectPoolArrow.Count;
				}
			}
			else
			{
				objectPoolArrow[curCount].transform.position = transform.position;
				objectPoolArrow[curCount].direction = rozLine * (weapon.arrowSpeed + playerWeaponStat.arrowSpeed);
				objectPoolArrow[curCount].damage = weapon.arrowDamage + playerWeaponStat.arrowDamage;
				objectPoolArrow[curCount].gameObject.SetActive(true);
				objectPoolCount -= 1;
				curCount++;
				if (curCount >= objectPoolArrow.Count)
				{
					curCount -= objectPoolArrow.Count;
				}
			}
		}
	}

    #endregion

    #region SpeedUp, DelayUp, DamageUp, ValueUp 메서드 → 레벨 업 후 생성되는 UI에서 무기 관련 업그레이드 기능들을 선택할 때, 해당 부분에 대한 값들을 적용하는 기능
    public void SpeedUp(float value = 1)
	{
		weapon.arrowSpeed += value;
	}
	public void DelayUp(float value = 1)
	{
		weapon.attackDelay += (value * weapon.attackDelay);
	}
	public void DamageUp(float value = 1)
	{
		weapon.arrowDamage += value;
	}

	// 투사체 발사 시, 투사체의 개수 증가
	public void ValueUp(int value = 1)
	{
		weapon.arrowValue += value;
	}

	public void pooling()
	{
		objectPoolCount += 1;
	}

    #endregion
}