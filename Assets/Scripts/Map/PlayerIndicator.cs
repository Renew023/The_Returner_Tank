using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIndicator : MonoBehaviour
{
    public static PlayerIndicator Instance { get; private set; }
    public GameObject indicatorPrefab;
    private RectTransform ind;
    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnLoaded; } else Destroy(gameObject);
    }
    private void OnLoaded(Scene sc, LoadSceneMode m)
    {
        if (sc.name == "MapScene" && ind != null)
        {
            var c = GameObject.Find("Canvas/StageContainer")?.GetComponent<RectTransform>();
            ind.SetParent(c, false);
        }
    }
    public void Restore() { if (ind != null) ind.SetParent(GameObject.Find("Canvas/StageContainer").GetComponent<RectTransform>(), false); }
    public bool CanSelect(int r, int cur) { return ind == null ? r == 0 : r > cur; }
    public void Select(int r, int c, Vector2 p) { if (ind == null) { var go = Instantiate(indicatorPrefab); DontDestroyOnLoad(go); ind = go.GetComponent<RectTransform>(); } ind.anchoredPosition = p; }
}