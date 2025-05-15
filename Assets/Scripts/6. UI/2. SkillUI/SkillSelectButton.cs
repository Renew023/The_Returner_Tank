using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectButton : MonoBehaviour
{
    #region SkillSelectButton 객체 변수 선언
    public Player player;
    public Skill skill;
    public Button skillSelect;
    public Image image;
    public TextMeshProUGUI title;

    #endregion

    #region Awake 메서드
    void Awake()
    {
        skillSelect.onClick.AddListener(Pick);
    }

    #endregion

    #region OnEnable, OnDisable 메서드
    void OnEnable()
    {
        if (DataManager.instance.curPlayerSkillMax == DataManager.instance.maxPlayerSkill)
        {
            title.text = "최대치";

            return;
        }

        if (skill.weaponCon != null)
        {
            image.sprite = skill.weaponCon.weaponSprite.sprite;
        }

        else
        {
            string skillIconName = skill.levelSkills[skill.level].upgradeType.ToString();

            image.sprite = Resources.Load<Sprite>("SkillIcons/" + skillIconName);
        }

        title.text = skill.levelSkills[skill.level].skillName;
    }

    void OnDisable()
    {
        skill = null;
    }

    #endregion

    #region Pick 메서드
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

        #region switch 문
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
            case SkillType.PlayerDelayUp:
                player.playerValue.playerWeaponStat.attackDelay += (int)value;
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

                break;
            case SkillType.ArrowDelayUp:
                foreach (var weapon in player.playerValue.weapons)
                {
                    if (skill.weaponCon.weapon.name == weapon.weapon.name)
                    {
                        weapon.DelayUp((int)value);
                        break;
                    }
                }
                break;
        }

        #endregion

        if (skill.level == 0)
        {
            UIManager.Instance.uiController.pauseUI.skillSlots[UIManager.Instance.uiController.pauseUI.skillsCount].sprite = image.sprite;
            UIManager.Instance.uiController.pauseUI.SetSkillImages(UIManager.Instance.uiController.pauseUI.skillsCount);
        }

        Time.timeScale = 1.0f;

        skill.level += 1;

        if (skill.level == 2)
        {
            DataManager.instance.curPlayerSkillMax += 1;
        }

        player.skillSelectUI.SetActive(false);
    }

    #endregion
}