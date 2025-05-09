// Assets/Scripts/GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ���� Map ���� �÷���. 
    /// MapScene�� ó�� �� ��� true, 
    /// Battle/Heal ������ ���ƿ��� false�� ����.
    /// </summary>
    public static bool IsFirstMapEntry = true;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
