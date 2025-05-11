using UnityEngine;

public class MapFollowCamera : MonoBehaviour
{
    public RectTransform playerIndicator;   // MapManager가 할당해 줍니다
    public float followSpeed = 2f;          // 따라오는 속도
    public float yOffset = 0f;              // 인디케이터 대비 Y 오프셋
    public float minY = -3f, maxY = 3f;     // 이동 범위 클램프

    private Vector3 basePos;

    void Start()
    {
        basePos = transform.position;       // 맵씬 카메라 초기 위치 저장
    }

    void LateUpdate()
    {
        if (playerIndicator == null) return;

        // 목표 Y 계산 & 범위 제한
        float targetY = Mathf.Clamp(
            playerIndicator.anchoredPosition.y + yOffset,
            minY, maxY
        );

        // X,Z 고정, Y만 보간
        Vector3 desired = new Vector3(basePos.x, targetY, basePos.z);
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );
    }
}
