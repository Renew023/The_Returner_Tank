using System.Collections;
using UnityEngine;

public class MapFollowCamera : MonoBehaviour
{
    [Header("References")]
    public RectTransform playerIndicator;   // MapManager가 런타임에 할당

    [Header("Pan Settings")]
    public float panHeight = 300f;        // 위로 팬할 높이
    public float panDuration = 2f;          // 올라가고 내려오는 시간

    [Header("Follow Settings")]
    public float followSpeed = 2f;          // 플레이어 따라다니기 속도

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(PanAndFollow());
    }

    IEnumerator PanAndFollow()
    {
        // 1) 위로 천천히 팬
        Vector3 upPos = initialPos + Vector3.up * panHeight;
        float t = 0f;
        while (t < panDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, upPos, t / panDuration);
            yield return null;
        }

        // 2) 다시 초기 위치로 복귀
        t = 0f;
        while (t < panDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(upPos, initialPos, t / panDuration);
            yield return null;
        }

        // 3) 플레이어 인디케이터 따라다니기
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
