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

	// Start is called before the first frame update
	public void ChangeState(StartUIState currentState)
	{
		mainUI.gameObject.SetActive(StartUIState.Main == currentState);
		weaponSelectUI.gameObject.SetActive(StartUIState.WeaponSelect == currentState);

		//switch (currentState)
		//{
		//	case StartUIState.Main:
		//		mainUI.gameObject.SetActive(true);
		//		break;
		//	case StartUIState.WeaponSelect:
		//		weaponSelectUI.gameObject.SetActive(true);
		//		break;
		//}
	}
}
