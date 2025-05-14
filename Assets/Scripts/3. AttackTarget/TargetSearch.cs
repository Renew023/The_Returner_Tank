using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class TargetSearch : MonoBehaviour
{
    #region TargetSearch 객체 변수 선언
    public float radius = 3.0f;
	public GameObject monsterShowPrefab;
	public LayerMask enemyLayer;
	public Collider2D[] colliders;
	public List<GameObject> monsterShow;
	public GameObject target;

    #endregion

    #region LateUpdate 메서드 →  지정된 반경 내의 적을 찾아 monsterShow 리스트에서 해당 위치에 맞는 객체를 활성화 또는 비활성화하는 기능 추가
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

    #endregion

    #region OnDrawGizmos 메서드 → 반경을 시각적으로 표시하기 위해 빨간 원을 그립니다.
    private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

    #endregion
}