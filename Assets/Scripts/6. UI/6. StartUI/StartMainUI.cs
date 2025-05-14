using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMainUI : MonoBehaviour
{
    [SerializeField] StartUI startUI;

    [SerializeField] private Button startButton;
    [SerializeField] private Button selectWeaponButton;

    void Awake()
    {
        selectWeaponButton.onClick.AddListener(()=>startUI.ChangeState(StartUIState.WeaponSelect));
    }
}