using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    public Player player;

    public List<int> value;
    public List<SkillSelectButton> skillSelectButton;
    public List<Skill> skills;
    public int ShowCard = 3;

    void OnEnable()
    {
        if (DataManager.instance.maxPlayerSkill - DataManager.instance.curPlayerSkillMax < 3)
            ShowCard = DataManager.instance.maxPlayerSkill - DataManager.instance.curPlayerSkillMax;

		while (skills.Count < ShowCard)
		{
            int rand;
            bool isNotEqual = true;
            bool isMaxSkill =
            DataManager.instance.maxPlayerSkill == player.playerValue.playerSkill.Count ? true : false;

            if (isMaxSkill)
                rand = Random.Range(0, player.playerValue.playerSkill.Count);
            else
                rand = Random.Range(0, DataManager.instance.skillList.Count); // 0~9 »çÀÌ

            for (int j = 0; j < value.Count; j++)
            {
                if (value[j] == rand)
                {
                    isNotEqual = false;
                    break;
                }
            }

            if (isNotEqual == true)
            {
                if (isMaxSkill)
                {
                    if (player.playerValue.playerSkill[rand].level == 3)
                        continue;

					skills.Add(player.playerValue.playerSkill[rand]);
				}
                else
                {
					if (DataManager.instance.skillList[rand].level == 3)
						continue;
					skills.Add(DataManager.instance.skillList[rand]);
                }
				value.Add(rand);
			}
            
		}

        if (ShowCard == 0)
			skillSelectButton[0].gameObject.SetActive(true);

		for (int i = 0; i < ShowCard; i++)
        {
            skillSelectButton[i].skill = skills[i];
            skillSelectButton[i].gameObject.SetActive(true);
        }
	}

    public void OnDisable()
    {
        skills.Clear();
        value.Clear();
		if (ShowCard == 0)
			skillSelectButton[0].gameObject.SetActive(false);

		for (int i = 0; i < ShowCard; i++)
		{
			skillSelectButton[i].gameObject.SetActive(false);
		}
	}
}
