using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartUIState
{
	Main,
	Setting,
	WeaponSelect
}

public class StartUI : MonoBehaviour
{
    [SerializeField] private StartMainUI mainUI;
	[SerializeField] private StartWeaponSelectUI weaponSelectUI;

	public void ChangeState(StartUIState currentState)
	{
		mainUI.gameObject.SetActive(StartUIState.Main == currentState);
		weaponSelectUI.gameObject.SetActive(StartUIState.WeaponSelect == currentState);
	}
}