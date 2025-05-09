using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header ("�������� ����")]
    //  ���� ���� ���� �������� �ε���
    public int currentStageIndex;

    //  ���� �������� Ÿ�� (�Ϲ� / ����)
    public StageType currentStageType;

    //  ���� ���̵�
    public int dungeonLevel;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            instance = this;

            //  �� ��ȯ�ص� ������ �� �ֵ��� DDOL�� ����
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetStageInfo(int stageIndex, StageType type, int level)
    {
        currentStageIndex = stageIndex;
        currentStageType = type;
        dungeonLevel = level;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
