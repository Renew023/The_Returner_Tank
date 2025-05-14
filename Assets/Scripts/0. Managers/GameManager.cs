using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region GameManager 변수 선언
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    [Header("Map Entry Flag")]
    [SerializeField]
    private bool firstMapEntry = true;
    public bool FirstMapEntry => firstMapEntry;

    public void DisableFirstMapEntry() => firstMapEntry = false;

    [Header("Stage Info")]
    //  스테이지 인덱스 추가
    public int stageIndex;
    // 현재 플레이어가 위치한 스테이지 인덱스 
    public int currentStageIndex;
    // 현재 스테이지 타입 (Enemy / Heal / Boss)
    public StageType currentStageType;
    //  던전 난이도 (스테이지 별 설정 값)
    public int dungeonLevel = 1;

    #endregion

    #region Awake 메서드
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

    #endregion

    #region SetStageInfo 메서드 → 던전 씬 입장할 때마다 스테이지 정보를 세팅하는 메서드
    //  스테이지 정보 초기화용 메서드 → 스테이지 선택 시 사용한다.
    public void SetStageInfo(int stageIndex, StageType stageType, int level)
    {
        //  stageIndex 설정
        this.stageIndex = stageIndex;
        currentStageIndex = stageIndex;
        currentStageType = stageType;
        dungeonLevel = level;
    }

    #endregion

    #region IncreaseDungeonLevel 메서드 → 던전 클리어 시, 던전의 레벨을 올려주는 기능
    //  던전 클리어 시, 던전 레벨을 증가시켜주는 메서드
    public void IncreaseDungeonLevel()
    {
        dungeonLevel++;
        Debug.Log($"[GameManager] 던전 레벨이 {dungeonLevel}로 증가했습니다.");
    }

    #endregion
}
