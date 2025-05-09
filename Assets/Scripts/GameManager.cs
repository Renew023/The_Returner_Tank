using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Map Entry Flag")]
    [SerializeField]
    private bool firstMapEntry = true;
    public bool FirstMapEntry => firstMapEntry;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
