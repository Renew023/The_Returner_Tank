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

    public int showCard = 3;

    void OnEnable()
    {
		bool isMax = DataManager.instance.maxPlayerSkill == player.playerSkill.Count ? true : false;

        if (DataManager.instance.curPlayerSkillMax - DataManager.instance.maxPlayerSkill < 3)
            showCard = DataManager.instance.maxPlayerSkill - DataManager.instance.curPlayerSkillMax;

		while (skills.Count < showCard)
        {
            bool isNotEqual = true;
            
			int rand = Random.Range(0, DataManager.instance.skillList.Count);

			if (isMax)
            {
                rand = Random.Range(0, player.playerSkill.Count);
            }


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
                if (isMax)
                {
					if (player.playerSkill[rand].level == 3)
						continue;
					skills.Add(player.playerSkill[rand]);
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
        if (showCard == 0)
            skillSelectButton[0].gameObject.SetActive(true);


        for (int i = 0; i < showCard; i++)
        {
            skillSelectButton[i].skill = skills[i];
            skillSelectButton[i].gameObject.SetActive(true);
        }
	}

    public void OnDisable()
    {
        skills.Clear();
        value.Clear();
		for (int i = 0; i < showCard; i++)
		{
			skillSelectButton[i].gameObject.SetActive(false);
		}
	}



    void Pick()
    {
    }
}
