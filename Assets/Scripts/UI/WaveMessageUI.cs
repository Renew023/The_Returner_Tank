using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveMessageUI : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 페이드 효과용
    public Text messageText;        // "Wave1", "Wave2", "CLEAR" 같은 텍스트
    public float fadeDuration = 1f;
    public float showDuration = 2f;

    // 외부에서 호출해서 메시지를 표시하는 함수
    public void ShowMessage(string message)
    {
        messageText.text = message;
        StopAllCoroutines();
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        yield return StartCoroutine(Fade(0f, 1f)); // 페이드 인
        yield return new WaitForSeconds(showDuration); // 유지
        yield return StartCoroutine(Fade(1f, 0f)); // 페이드 아웃
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
