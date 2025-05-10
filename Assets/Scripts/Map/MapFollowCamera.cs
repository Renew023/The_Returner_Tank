using UnityEngine;

public class MapFollowCamera : MonoBehaviour
{
    [Header("References")]
    public RectTransform playerIndicator;

    [Header("Follow Settings")]
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public float minY = -3f;
    public float maxY = 5f;

    private Vector3 basePos;

    void Start()
    {
        // 초기 카메라 위치 저장
        basePos = transform.position;
    }

    void LateUpdate()
    {
        if (playerIndicator == null) return;

        // 플레이어 위치 기반 목표 Y 계산 및 클램프
        float targetY = Mathf.Clamp(
            playerIndicator.anchoredPosition.y + yOffset,
            minY, maxY
        );

        // X,Z 는 고정, Y만 따라가기
        Vector3 desired = new Vector3(basePos.x, targetY, basePos.z);

        // 부드럽게 보간
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );
    }
}
