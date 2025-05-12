using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public SpriteRenderer weaponSprite;
	public Weapon weapon;
	[SerializeField] private List<Arrow> objectPoolArrow;
	[SerializeField] private int objectPoolCount = 0;
	[SerializeField] private int curCount = 0;
    [SerializeField] public Vector2 targetDirect;
	public Weapon playerWeaponStat;
	
	public Arrow arrow;

	void Awake()
    {
    }

	void LateUpdate()
    {
		if (weapon.timer > (weapon.attackDelay - playerWeaponStat.attackDelay))
		{
			Attack(targetDirect);
			weapon.timer = 0f;
		}
		else
		{
			weapon.timer += Time.deltaTime;
		}
	}

    public void Attack(Vector2 dir)
    {
		int value = (weapon.arrowValue + playerWeaponStat.arrowValue);
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
				//objectPoolArrow[curCount].gameObject.SetActive(false);
				//objectPoolArrow[curCount].transform.position = transform.position;
				//objectPoolArrow[curCount].direction = rozLine * arrowSpeed;
				//objectPoolArrow[curCount].damage = arrowDamage;
				//objectPoolArrow[curCount].gameObject.SetActive(true);
				curCount++;
				if (curCount >= objectPoolArrow.Count)
				{
					curCount -= objectPoolArrow.Count;
				}
			}
			else
			{
				objectPoolArrow[curCount].transform.position = transform.position;
				objectPoolArrow[curCount].direction = rozLine * (weapon.arrowSpeed +playerWeaponStat.arrowSpeed);
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

    public void SpeedUp(float value=1)
    {
		weapon.arrowSpeed += value;
    }
    public void DamageUp(float value = 1)
    {
		weapon.arrowDamage += value;
	}

    public void ValueUp(int value = 1)
    {
		weapon.arrowValue += value;
	}

    public void pooling()
    {
        objectPoolCount += 1;
    }
    }

