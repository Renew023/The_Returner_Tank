using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    #region FollowCamera 변수 선언
    [SerializeField] private Transform target;
	[SerializeField] private Camera camera;

	private float offsetX;
	private float offsetY;

	[SerializeField] private Vector2 minCameraRange;
	[SerializeField] private Vector2 maxCameraRange;

    #endregion

    #region Awake 메서드
    void Awake()
	{
	}

    #endregion

    #region OnEnable 메서드
    void OnEnable()
	{
		target = GameObject.Find("Player").GetComponent<Transform>();
	}

    #endregion

    #region Start 메서드
    void Start()
	{
		minCameraRange.x += camera.orthographicSize * Screen.width / Screen.height;
		maxCameraRange.x += -(camera.orthographicSize * Screen.width / Screen.height) + 1;

		minCameraRange.y += camera.orthographicSize;
		maxCameraRange.y += -(camera.orthographicSize);
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

		offsetX = transform.position.x - target.position.x;
		offsetY = transform.position.y - target.position.y;
	}

    #endregion

    #region LateUpdate 메서드 → 플레이어를 추적하는 기능이 들어있습니다.
    void LateUpdate()
	{
		float frame;
		Vector3 pos = transform.position;

		pos.x = target.position.x + offsetX;
		pos.y = target.position.y + offsetY;

		pos.x = Mathf.Clamp(pos.x, minCameraRange.x, maxCameraRange.x);
		pos.y = Mathf.Clamp(pos.y, minCameraRange.y, maxCameraRange.y);

		transform.position = Vector3.Lerp(transform.position, pos, 1/5f/5f);
	}

    #endregion
}