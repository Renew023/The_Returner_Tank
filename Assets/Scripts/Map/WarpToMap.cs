using UnityEngine;

public class WarpToMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // SceneController �� ��ϵ� ToMap() ȣ��
        SceneController.ToMap();
    }
}
