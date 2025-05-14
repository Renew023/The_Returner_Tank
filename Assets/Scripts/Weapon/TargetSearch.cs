using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class TargetSearch : MonoBehaviour
{
	public float radius = 3.0f;
	public GameObject monsterShowPrefab;
	public LayerMask enemyLayer;
	public Collider2D[] colliders;
	public List<GameObject> monsterShow;
	public GameObject target;

	void LateUpdate()
	{
		colliders = Physics2D.OverlapCircleAll(target.transform.position, 100.0f ,enemyLayer);
		for (int i = monsterShow.Count - 1; i >= colliders.Length; i--)
		{
			monsterShow[i].SetActive(false);
		}

		for (int j = 0; j < monsterShow.Count; j++)
		{
			if (colliders.Length == j)
				break;
			Vector2 collider = colliders[j].transform.localPosition;
			monsterShow[j].SetActive(true);
			monsterShow[j].transform.localPosition = collider;
		}

		for(int i = monsterShow.Count; i < colliders.Length; i++) 
		{
			Vector2 collider = colliders[i].transform.localPosition;
			monsterShow.Add(Instantiate(monsterShowPrefab, collider, Quaternion.identity, transform));
			monsterShow[i].transform.localPosition = collider;
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
