using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    PlayerHpUp,
    PlayerSpeedUp,
    PlayerArrowSpeedUp,
    PlayerArrowValueUp,
	PlayerArrowDamageUp,

    ArrowSpeedUp,
    ArrowDamageUp,
    ArrowValueUp

}

[System.Serializable]
public struct LevelSkill
{
    public string skillName;
    public string skillExplain;
    public SkillType upgradeType;
    public float value;
}

[System.Serializable]
public class Skill
{
    public string name;
    public Player player;
    public List<LevelSkill> levelSkills = new List<LevelSkill>();
    public int level = 0;
    public WeaponController weaponCon;

    public Skill(List<LevelSkill> levelSkills, int level)
    {
        this.levelSkills = levelSkills;
        this.level = level;
    }

    public Skill(Skill skill)
    {
        player = skill.player;
        levelSkills = skill.levelSkills;
        level = skill.level;
        weaponCon = weaponCon;
    }

    public Skill()
    {

    }

    //void PickSkill()
    //{
    //    switch (levelSkills[Level].upgradeType)
    //    {
    //        case SkillType.PlayerHpUp:
    //            player.HpUp(value);
    //            break;
    //        case SkillType.PlayerSpeedUp:
    //            break;
    //        case SkillType.ArrowSpeedUp:
    //            if (Level == 0)
    //                player.weapons.Add(Instantiate(weapon, player.gameObject.transform.position, Quaternion.identity, player.gameObject.transform.parent));
    //            //player.weaponController.SpeedUp();
				//break;
    //        case SkillType.ArrowValueUp:
				////player.weaponController.ValueUp();
				//break;
    //        case SkillType.ArrowDamageUp:
    //            //player.weaponController.DamageUp();
				//break;

    //    }
    //}
}
