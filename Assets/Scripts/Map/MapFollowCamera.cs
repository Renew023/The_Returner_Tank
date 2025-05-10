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
        // �ʱ� ī�޶� ��ġ ����
        basePos = transform.position;
    }

    void LateUpdate()
    {
        if (playerIndicator == null) return;

        // �÷��̾� ��ġ ��� ��ǥ Y ��� �� Ŭ����
        float targetY = Mathf.Clamp(
            playerIndicator.anchoredPosition.y + yOffset,
            minY, maxY
        );

        // X,Z �� ����, Y�� ���󰡱�
        Vector3 desired = new Vector3(basePos.x, targetY, basePos.z);

        // �ε巴�� ����
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );
    }
}
