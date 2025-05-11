using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class AttackTarget : MonoBehaviour
{
	public float radius = 3.0f;
	public LayerMask layer;
	public Collider2D[] colliders;
	public Collider2D short_enemy;

	public Vector2 Searching(Vector2 look, Vector2 user)
	{
		//Collider[] colliders;
		//Collider short_enemy;
		colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);

		if (colliders.Length > 0)
		{
			float short_distance = Vector2.Distance(transform.position, colliders[0].transform.position);
			short_enemy = colliders[0];
			foreach (Collider2D col in colliders)
			{
				float short_distance2 = Vector3.Distance(transform.position, col.transform.position);
				if (short_distance > short_distance2)
				{
					short_distance = short_distance2;
					short_enemy = col;
				}
			}
			return short_enemy.transform.position - (Vector3)user;
		}
		return look;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
