using UnityEngine;


public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }

    [Header("Map Entry Flag")]
    [SerializeField]
    private bool firstMapEntry = true;
    public bool FirstMapEntry => firstMapEntry;

    public void DisableFirstMapEntry() => firstMapEntry = false;

    [Header("Stage Info")]
    // ���� �÷��̾ ��ġ�� �������� �ε��� 
    public int currentStageIndex;
    // ���� �������� Ÿ�� (Enemy / Heal / Boss)
    public StageType currentStage;

    private void Awake()
    {
        // �̹� �ν��Ͻ��� ������ �ߺ� �ı�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
