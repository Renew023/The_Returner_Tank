// Assets/Scripts/GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 최초 Map 진입 플래그. 
    /// MapScene을 처음 연 경우 true, 
    /// Battle/Heal 씬에서 돌아오면 false로 세팅.
    /// </summary>
    public static bool IsFirstMapEntry = true;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
