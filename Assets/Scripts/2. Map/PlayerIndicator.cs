using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIndicator : MonoBehaviour
{
    #region PlayerIndicator 객체 변수 선언
    public static PlayerIndicator Instance { get; private set; }
    public GameObject indicatorPrefab;
    private RectTransform ind;

    #endregion

    #region Awake 메서드
    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnLoaded; } else Destroy(gameObject);
    }

    #endregion

    #region OnLoaded 메서드 → MapScene이 로드될 때 인디케이터의 부모를 StageContainer로 재지정하는 기능
    private void OnLoaded(Scene sc, LoadSceneMode m)
    {
        if (sc.name == "MapScene" && ind != null)
        {
            var c = GameObject.Find("Canvas/StageContainer")?.GetComponent<RectTransform>();
            ind.SetParent(c, false);
        }
    }

    #endregion

    // 인디케이터를 다시 StageContainer에 붙임 (재귀적 복원용)
    public void Restore() { if (ind != null) ind.SetParent(GameObject.Find("Canvas/StageContainer").GetComponent<RectTransform>(), false); }

    // 선택 가능한 노드인지 여부 반환 (현재 인디케이터가 없으면 첫 줄만 선택 가능)
    public bool CanSelect(int r, int cur) { return ind == null ? r == 0 : r > cur; }

    // 인디케이터를 생성하거나 위치를 갱신함
    public void Select(int r, int c, Vector2 p) { if (ind == null) { var go = Instantiate(indicatorPrefab); DontDestroyOnLoad(go); ind = go.GetComponent<RectTransform>(); } ind.anchoredPosition = p; }
}