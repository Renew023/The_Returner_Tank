using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MapFollowCamera : MonoBehaviour
{
    [Header("���� ���")]
    [Tooltip("Hierarchy �� NodesCanvas �� StageContainer �� PlayerIndicator")]
    public RectTransform target;

    [Header("ī�޶� ������")]
    public Vector3 offset = new Vector3(0, 0, -10f);

    [Header("�ε巯�� ���� �ӵ�")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        // 1) �ε������� ���� ��ġ ���ϱ�
        Vector3 targetWorldPos = target.TransformPoint(Vector3.zero) + offset;

        // 2) ���ϴ� ������: X,Z�� ����
        float fixedX = transform.position.x;
        float fixedZ = transform.position.z;
        Vector3 desiredPos = new Vector3(fixedX, targetWorldPos.y, fixedZ);

        // 3) �ε巴�� ����
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
    }
}