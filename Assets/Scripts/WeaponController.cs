using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public Arrow Bomb;
	[SerializeField] private List<Arrow> objectPoolArrow;
	[SerializeField] private int objectPoolCount = 0;
	[SerializeField] private int curCount = 0;

    public void SetArrow(Arrow weapon)
    {
        Bomb = weapon;
    }
    public void Attack(int value, Vector2 dir, float speed, float damage)
    {
        Vector2 rozLine = dir.normalized + (new Vector2(0, 3) * (value - 1));

        if (objectPoolCount == 0)
        {
            objectPoolArrow.Add(Instantiate(Bomb, transform.position, Quaternion.identity));
            objectPoolArrow[curCount].countReturn = this;
            objectPoolArrow[curCount].owner = transform.parent.gameObject;

            objectPoolArrow[curCount].transform.position = transform.position;
            objectPoolArrow[curCount].direction = dir.normalized * speed;
			objectPoolArrow[curCount].damage = damage;
			curCount++;
        }
        else
        {
            if (curCount >= objectPoolArrow.Count)
            {
                curCount -= objectPoolArrow.Count;
            }
            objectPoolArrow[curCount].gameObject.SetActive(true);

			objectPoolArrow[curCount].transform.position = transform.position;
            objectPoolArrow[curCount].direction = dir.normalized * speed;
			objectPoolArrow[curCount].damage = damage;
			curCount++;
			objectPoolCount -= 1;
        }
    }

    public void pooling()
    {
        objectPoolCount += 1;
    }
    }

