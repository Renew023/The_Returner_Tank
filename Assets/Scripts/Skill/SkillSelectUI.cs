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

    void OnEnable()
    {
		while (skills.Count < 3)
		{
            bool isNotEqual = true;
			int rand = Random.Range(0, player.skillList.Count); // 0~9 »çÀÌ

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
                value.Add(rand);
                skills.Add(player.skillList[rand]);
            }
            
		}

        for (int i = 0; i < skillSelectButton.Count; i++)
        {
            skillSelectButton[i].skill = skills[i];
            skillSelectButton[i].gameObject.SetActive(true);
        }
	}

    public void OnDisable()
    {
        skills.Clear();
        value.Clear();
		for (int i = 0; i < skillSelectButton.Count; i++)
		{
			skillSelectButton[i].gameObject.SetActive(false);
		}
	}



    void Pick()
    {
    }
}
