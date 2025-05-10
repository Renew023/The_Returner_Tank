using UnityEngine;


public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    [Header("Map Entry Flag")]
    [SerializeField]
    private bool firstMapEntry = true;
    public bool FirstMapEntry => firstMapEntry;

    [Header("Stage Info")]
    // 현재 플레이어가 위치한 스테이지 인덱스 
    public int currentStageIndex;
    // 현재 스테이지 타입 (Enemy / Heal / Boss)
    public StageType currentStageType;
    //  던전 난이도 (스테이지 별 설정 값)
    public int dungeonLevel;

    private void Awake()
    {
        // 이미 인스턴스가 있으면 중복 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetStageInfo(int stageIndex, StageType stageType, int level)
    {
        currentStageIndex = stageIndex;
        currentStageType = stageType;
        dungeonLevel = level;
    }
}
