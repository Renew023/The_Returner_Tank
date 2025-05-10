using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : Weapon
{
	[SerializeField] private List<Arrow> objectPoolArrow;
	[SerializeField] private int objectPoolCount = 0;
	[SerializeField] private int curCount = 0;
    [SerializeField] private Vector2 targetDirect;

    void LateUpdate()
    {
    }

    public void SetArrow(Arrow weapon)
    {
        arrow = weapon;
        objectPoolArrow.Clear();
        objectPoolCount = 0;
        curCount = 0;
    }



    public void Attack(Vector2 dir)
    {
        Vector2 rozLine = dir.normalized;

            if (objectPoolCount < 5)
            {
			    arrow.countReturn = this;
			    arrow.owner = transform.parent.gameObject;

			    objectPoolArrow.Add(Instantiate(arrow, transform.position, Quaternion.identity));

                objectPoolArrow[curCount].transform.position = transform.position;
                objectPoolArrow[curCount].direction = rozLine * arrowSpeed;
                objectPoolArrow[curCount].damage = arrowDamage;
			    objectPoolArrow[curCount].gameObject.SetActive(true);
			curCount++;
			    if (curCount >= objectPoolArrow.Count)
			    {
				    curCount -= objectPoolArrow.Count;
			    }
            }
            else
            {
                objectPoolArrow[curCount].transform.position = transform.position;
                objectPoolArrow[curCount].direction = rozLine * arrowSpeed;
                objectPoolArrow[curCount].damage = arrowDamage;
                objectPoolArrow[curCount].gameObject.SetActive(true);
                objectPoolCount -= 1;
                curCount++;
			    if (curCount >= objectPoolArrow.Count)
			    {
				    curCount -= objectPoolArrow.Count;
			    }
		}
    }

    public void pooling()
    {
        objectPoolCount += 1;
    }
    }

