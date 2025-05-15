using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SkillType →  // 플레이어와 무기 관련 다양한 능력치 업그레이드 종류
public enum SkillType
{
    PlayerHpUp,
    PlayerSpeedUp,
    PlayerArrowSpeedUp,
    PlayerArrowValueUp,
	PlayerArrowDamageUp,
    PlayerDelayUp,

    ArrowSpeedUp,
    ArrowDamageUp,
    ArrowValueUp,
    ArrowDelayUp
}

#endregion

#region LevelSkill 구조체 → 스킬 구조체
[System.Serializable]
public struct LevelSkill
{
    public string skillName;
    public string skillExplain;
    public SkillType upgradeType;
    public float value;
}

#endregion

#region Skill 클래스 → 플레이어가 사용할 스킬들 변수들
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

    #endregion
}
