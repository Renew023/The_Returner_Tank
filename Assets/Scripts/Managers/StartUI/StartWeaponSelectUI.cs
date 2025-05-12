using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class StartWeaponSelectUI : MonoBehaviour
{
	[SerializeField] StartUI startUI;

	[SerializeField] private Image weaponImage;
    [SerializeField] private Image arrowImage;
    [SerializeField] private TextMeshProUGUI ItemName;

    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;

	


	// Start is called before the first frame update
	void Awake()
    {
        ItemName.text = DataManager.instance.skill.weaponCon.weapon.name.ToString();
        weaponImage.sprite = DataManager.instance.skill.weaponCon.weaponSprite.sprite;
        arrowImage.sprite = DataManager.instance.skill.weaponCon.arrow.GetComponentInChildren<SpriteRenderer>().sprite;
        prevButton.onClick.AddListener(() => Change(-1));
		nextButton.onClick.AddListener(() => Change(1));
		exitButton.onClick.AddListener(() => startUI.ChangeState(StartUIState.Main));
    }

	public void Change(int value)
	{
		int skillValue = DataManager.instance.skillValue;

		skillValue += value;
		if (skillValue >= DataManager.instance.skillList.Count)
		{
			skillValue = 0;
		}
		else if (skillValue < 0)
		{
			skillValue = DataManager.instance.skillList.Count -1;
		}
		DataManager.instance.skillValue = skillValue;
		DataManager.instance.skill = DataManager.instance.skillList[skillValue];
		ShowReset();
	}

	void ShowReset()
	{
		ItemName.text = DataManager.instance.skill.weaponCon.weapon.name.ToString();
		weaponImage.sprite = DataManager.instance.skill.weaponCon.weaponSprite.sprite;
		arrowImage.sprite = DataManager.instance.skill.weaponCon.arrow.GetComponentInChildren<SpriteRenderer>().sprite;
	}


	// Update is called once per frame
	void Update()
    {
        
    }
}
