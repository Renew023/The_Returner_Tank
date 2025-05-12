using UnityEngine;

public class MapFollowCamera : MonoBehaviour
{
    public RectTransform playerIndicator;   // MapManager�� �Ҵ��� �ݴϴ�
    public float followSpeed = 2f;          // ������� �ӵ�
    public float yOffset = 0f;              // �ε������� ��� Y ������
    public float minY = -3f, maxY = 3f;     // �̵� ���� Ŭ����

    private Vector3 basePos;

    void Start()
    {
        basePos = transform.position;       // �ʾ� ī�޶� �ʱ� ��ġ ����
    }

    void LateUpdate()
    {
        if (playerIndicator == null) return;

        // ��ǥ Y ��� & ���� ����
        float targetY = Mathf.Clamp(
            playerIndicator.anchoredPosition.y + yOffset,
            minY, maxY
        );

        // X,Z ����, Y�� ����
        Vector3 desired = new Vector3(basePos.x, targetY, basePos.z);
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );
    }
}
