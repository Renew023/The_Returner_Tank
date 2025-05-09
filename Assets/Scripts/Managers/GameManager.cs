using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //  현재 진행 중인 스테이지 인덱스
    public int currentStageIndex;

    //  현재 스테이지 타입 (일반 / 보스)
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

            //  씬 전환해도 유지할 수 있도록 DDOL로 구현
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
