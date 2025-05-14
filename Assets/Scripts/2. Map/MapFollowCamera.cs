using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MapFollowCamera : MonoBehaviour
{
    [Header("추적 대상")]
    [Tooltip("Hierarchy ▶ NodesCanvas ▶ StageContainer ▶ PlayerIndicator")]
    public RectTransform target;

    [Header("카메라 오프셋")]
    public Vector3 offset = new Vector3(0, 0, -10f);

    [Header("부드러운 추적 속도")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        // 1) 인디케이터 월드 위치 구하기
        Vector3 targetWorldPos = target.TransformPoint(Vector3.zero) + offset;

        // 2) 원하는 포지션: X,Z는 고정
        float fixedX = transform.position.x;
        float fixedZ = transform.position.z;
        Vector3 desiredPos = new Vector3(fixedX, targetWorldPos.y, fixedZ);

        // 3) 부드럽게 보간
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
    }
}