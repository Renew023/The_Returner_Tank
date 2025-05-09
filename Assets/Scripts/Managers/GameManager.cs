using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //  ���� ���� ���� �������� �ε���
    public int currentStageIndex;

    //  ���� �������� Ÿ�� (�Ϲ� / ����)
    public StageType currentStage;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;

            //  �� ��ȯ�ص� ������ �� �ֵ��� DDOL�� ����
            DontDestroyOnLoad(gameObject);
        }
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
