using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class AttackTarget : MonoBehaviour
{
    #region AttackTarget 객체 변수 선언
    public float radius = 3.0f;
	public LayerMask wallLayer;
	public LayerMask enemyLayer;
	public Collider2D[] colliders;
	public Collider2D short_enemy;

    #endregion

    #region Searching 메서드 → 탐지 반경 내에서 벽에 가리지 않은 가장 가까운 적을 찾아 그 방향 벡터를 반환하는 기능
    public Vector2 Searching(Vector2 look, Vector2 user)
	{
		colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

		Collider2D closestEnemy = null;
		float closestDistance = float.MaxValue;

		foreach (Collider2D enemyCol in colliders)
		{
			Vector2 direction = (Vector2)enemyCol.transform.position - (Vector2)transform.position;
			float distance = direction.magnitude;

			// Raycast: 벽과 적을 모두 포함한 레이어에 대해 검사
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, wallLayer | enemyLayer);

			// 먼저 맞은 게 벽이면 무시, 적이면 유효 후보
			if (hit.collider != null && hit.collider.gameObject == enemyCol.gameObject)
			{
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closestEnemy = enemyCol;
				}
			}
		}

		if (closestEnemy != null)
		{
			short_enemy = closestEnemy;
			return closestEnemy.transform.position - (Vector3)user;
		}

		return look;
	}

    #endregion

    #region OnDrawGizmos 메서드 → 에디터 상에서 탐지 범위를 그려내는 기능
    private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

    #endregion
}
