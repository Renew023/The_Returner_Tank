using UnityEngine;


public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static GameManager Instance { get; private set; }

    [Header("Map Entry Flag")]
    [SerializeField]
    private bool firstMapEntry = true;
    public bool FirstMapEntry => firstMapEntry;

    [Header("Stage Info")]
    // ���� �÷��̾ ��ġ�� �������� �ε��� (0���� ����)
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

        // ���� �ν��Ͻ��� ����ϰ� �� ��ȯ �� �ı����� �ʵ��� ����
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
