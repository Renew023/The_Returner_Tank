using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectButton : MonoBehaviour
{
    public Player player;
    public Skill skill;
    public Button skillSelect;
    //public SpriteRenderer ab;
    public Image image;
    public TextMeshProUGUI title;

    void Awake()
    {
		skillSelect.onClick.AddListener(Pick);
	}

    void OnEnable()
    {
        //ab = skill.weapon.weaponSprite;
        if (DataManager.instance.curPlayerSkillMax == DataManager.instance.maxPlayerSkill)
        {
            title.text = "������";
            return;
        }


		if (skill.weaponCon != null)
            image.sprite = skill.weaponCon.weaponSprite.sprite;
        else
            image.sprite = null;

		title.text = skill.levelSkills[skill.level].skillName.ToString();
    }

    void OnDisable()
    {
        skill = null;
    }

    void Pick()
    {
        if (DataManager.instance.curPlayerSkillMax == DataManager.instance.maxPlayerSkill)
        {
            Time.timeScale = 1.0f;
			player.skillSelectUI.SetActive(false);
            return;
		}

        if (skill.level == 0)
        {
            player.playerValue.playerSkill.Add(skill);
            if (skill.weaponCon != null)
            {
                player.playerValue.weapons.Add(Instantiate(skill.weaponCon, player.transform.position, Quaternion.identity, player.transform));
            }
        }
        float value = skill.levelSkills[skill.level].value;


		switch (skill.levelSkills[skill.level].upgradeType)
        {
            case SkillType.PlayerHpUp:
                player.HpUp(value);
                break;
            case SkillType.PlayerSpeedUp:
                player.MoveSpeedUp();
				break;
            case SkillType.PlayerArrowSpeedUp:
                player.playerValue.playerWeaponStat.arrowSpeed += value;
                break;

            case SkillType.PlayerArrowDamageUp:
				player.playerValue.playerWeaponStat.arrowDamage += value;
				break;

            case SkillType.PlayerArrowValueUp:
				player.playerValue.playerWeaponStat.arrowValue += (int)value;
				break;





            case SkillType.ArrowSpeedUp:

                foreach (var weapon in player.playerValue.weapons)
                {
                    if (skill.weaponCon.weapon.name == weapon.weapon.name)
                    {
                        weapon.SpeedUp(value);
                        break;
                    }
                }
				//skill.weapon.SpeedUp(value);
                break;
            case SkillType.ArrowValueUp:
				foreach (var weapon in player.playerValue.weapons)
				{
					if (skill.weaponCon.weapon.name == weapon.weapon.name)
					{
						weapon.ValueUp((int)value);
						break;
					}
				}
				//skill.weapon.ValueUp((int)value);
				break;
            case SkillType.ArrowDamageUp:
				foreach (var weapon in player.playerValue.weapons)
				{
					if (skill.weaponCon.weapon.name == weapon.weapon.name)
					{
						weapon.DamageUp((int)value);
						break;
					}
				}
				//skill.weapon.DamageUp(value);
				break;

        }
        Time.timeScale = 1.0f;
        skill.level += 1;
        if (skill.level == 3)
        {
            DataManager.instance.curPlayerSkillMax += 1;
        }
        player.skillSelectUI.SetActive(false);
    }
}
