using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMiniMap : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private GameObject targetShow;
	//[SerializeField] private Camera camera;
	private float offsetX;
	private float offsetY;
	[SerializeField] private Vector2 minMiniMapRange;
	[SerializeField] private Vector2 maxMiniMapRange;
	private float scaleValue = 0.2f;

	void Awake()
	{
	}

	void OnEnable()
	{
		//target = GameObject.Find("Player").GetComponent<Transform>();
	}

	void Start()
	{
		//minCameraRange.x += camera.orthographicSize * Screen.width / Screen.height;
		//maxCameraRange.x += -(camera.orthographicSize * Screen.width / Screen.height) + 1;
		minMiniMapRange *= scaleValue;
		maxMiniMapRange *= scaleValue;

		minMiniMapRange.x += 6f * scaleValue;
		minMiniMapRange.y += 6f * scaleValue;

		maxMiniMapRange.x += -7f * scaleValue + scaleValue;
		maxMiniMapRange.y += -7f * scaleValue;


		//minCameraRange.y += camera.orthographicSize;
		//maxCameraRange.y += -(camera.orthographicSize);

		offsetX = transform.localPosition.x - (target.localPosition.x * scaleValue);//0 - 3
		offsetY = transform.localPosition.y - (target.localPosition.y * scaleValue);//0 - 3
	}


	void LateUpdate()
	{
		Vector3 pos = transform.localPosition; // 0 0 0


		pos.x = -1 *((target.localPosition.x * scaleValue) + offsetX);
		pos.y = -1 *((target.localPosition.y * scaleValue) + offsetY);
		targetShow.transform.localPosition = -pos * (1/scaleValue);

		pos.x = Mathf.Clamp(pos.x, minMiniMapRange.x, maxMiniMapRange.x);
		pos.y = Mathf.Clamp(pos.y, minMiniMapRange.y, maxMiniMapRange.y);

		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 1 / 5f / 5f);
	}
}

