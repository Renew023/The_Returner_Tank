using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMainUI : MonoBehaviour
{
    [SerializeField] StartUI startUI;

    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button selectWeaponButton;
    // Start is called before the first frame update
    void Awake()
    {
        //startButton.onClick.AddListener();
        settingButton.onClick.AddListener(() => startUI.ChangeState(StartUIState.Setting));
        selectWeaponButton.onClick.AddListener(()=>startUI.ChangeState(StartUIState.WeaponSelect));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
