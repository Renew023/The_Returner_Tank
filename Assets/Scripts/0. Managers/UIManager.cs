using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region UIManager 변수 선언
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject uiCanvas;
    public UIController uiController;

    #endregion

    #region Awake 메서드
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(uiCanvas);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
