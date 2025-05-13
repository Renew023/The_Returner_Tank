using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

	public Player player;
	public PlayerValue playerValue;
	public List<Weapon> playerWeapon = new List<Weapon>(5);

	public List<Skill> skillList = new List<Skill>(10);
	public List<Skill> WeaponSkillList = new List<Skill>(5);
    public Skill skill;
	public int skillValue = 0;
	public int maxPlayerSkill = 4;
	public int curPlayerSkillMax = 0;

	[Header("플레이어 정보")]
    public float savedPlayerHp;
    public float savedPlayerMaxHp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
			DontDestroyOnLoad(instance);
        }
        else
        {
			Destroy(gameObject);
		}
		WeaponSkillList = skillList.Where(skill => skill.weaponCon != null).ToList();
		skill = WeaponSkillList[0];
	}

	public void Pick()
	{
		if (playerValue.playerSkill.Count != 0)
			return;
		player = GameObject.Find("Player").GetComponent<Player>();
		//if (playerValue != null)
		//	player = playerValue;

		playerValue.playerSkill.Add(skill);
		playerWeapon.Add(skill.weaponCon.weapon);

		float value = skill.levelSkills[skill.level].value;


		switch (skill.levelSkills[skill.level].upgradeType)
		{
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
		skill.level += 1;
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
