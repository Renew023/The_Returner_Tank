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
    public void Attack(Vector2 roz, float damage)
    {
        if (objectPoolCount == 0)
        {
            objectPoolArrow.Add(Instantiate(Bomb, transform.position, Quaternion.identity));
            objectPoolArrow[curCount].transform.position = transform.position;
            objectPoolArrow[curCount].direction = roz.normalized;
			objectPoolArrow[curCount].speed = 5;
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
			objectPoolArrow[curCount].direction = roz.normalized;
			objectPoolArrow[curCount].speed = 5;
            curCount++;
			objectPoolCount -= 1;
        }
    }

    public void pooling()
    {
        objectPoolCount += 1;
    }
    }

