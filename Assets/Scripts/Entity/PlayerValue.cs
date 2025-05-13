using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerValue
{
	public Weapon playerWeaponStat;
	public List<Skill> playerSkill = new List<Skill>(5);
	public List<WeaponController> weapons = new List<WeaponController>(5);
	public float Exp;
	public float Level;
}
