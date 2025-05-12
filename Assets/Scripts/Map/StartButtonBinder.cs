using UnityEngine;
using UnityEngine.UI;

public class StartButtonBinder : MonoBehaviour
{
    [Tooltip("Hierarchy �� Canvas �� StartButton")]
    [SerializeField] private Button startButton;

    void Awake()
    {
        if (startButton == null)
            Debug.LogError("StartButtonBinder: Start Button�� �ν����Ϳ� �Ҵ��ϼ���.");
        else
            // static SceneController.ToMap �޼��带 �����ʷ� �߰�
            startButton.onClick.AddListener(SceneController.ToMap);
    }
}