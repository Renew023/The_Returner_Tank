using System.Collections;
using UnityEngine;

public class MapFollowCamera : MonoBehaviour
{
    [Header("References")]
    public RectTransform playerIndicator;   // MapManager�� ��Ÿ�ӿ� �Ҵ�

    [Header("Pan Settings")]
    public float panHeight = 300f;        // ���� ���� ����
    public float panDuration = 2f;          // �ö󰡰� �������� �ð�

    [Header("Follow Settings")]
    public float followSpeed = 2f;          // �÷��̾� ����ٴϱ� �ӵ�

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(PanAndFollow());
    }

    IEnumerator PanAndFollow()
    {
        // 1) ���� õõ�� ��
        Vector3 upPos = initialPos + Vector3.up * panHeight;
        float t = 0f;
        while (t < panDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, upPos, t / panDuration);
            yield return null;
        }

        // 2) �ٽ� �ʱ� ��ġ�� ����
        t = 0f;
        while (t < panDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(upPos, initialPos, t / panDuration);
            yield return null;
        }

        // 3) �÷��̾� �ε������� ����ٴϱ�
        while (true)
        {
            if (playerIndicator != null)
            {
                Vector3 camPos = transform.position;
                float targetY = playerIndicator.anchoredPosition.y;
                transform.position = Vector3.Lerp(
                    camPos,
                    new Vector3(camPos.x, targetY, camPos.z),
                    followSpeed * Time.deltaTime
                );
            }
            yield return null;
        }
    }
}
