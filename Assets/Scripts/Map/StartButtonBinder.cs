using UnityEngine;
using UnityEngine.UI;

public class StartButtonBinder : MonoBehaviour
{
    [Tooltip("Hierarchy ▶ Canvas ▶ StartButton")]
    [SerializeField] private Button startButton;

    void Awake()
    {
        if (startButton == null)
            Debug.LogError("StartButtonBinder: Start Button을 인스펙터에 할당하세요.");
        else
            // static SceneController.ToMap 메서드를 리스너로 추가
            startButton.onClick.AddListener(SceneController.ToMap);
    }
}