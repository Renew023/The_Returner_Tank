using UnityEngine;

public class WarpToMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // SceneController 에 등록된 ToMap() 호출
        SceneController.ToMap();
    }
}
