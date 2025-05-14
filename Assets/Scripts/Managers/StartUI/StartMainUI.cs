using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMainUI : MonoBehaviour
{
    [SerializeField] StartUI startUI;

    [SerializeField] private Button startButton;
    [SerializeField] private Button selectWeaponButton;
    // Start is called before the first frame update
    void Awake()
    {
        //startButton.onClick.AddListener();
        selectWeaponButton.onClick.AddListener(()=>startUI.ChangeState(StartUIState.WeaponSelect));
    }
}
