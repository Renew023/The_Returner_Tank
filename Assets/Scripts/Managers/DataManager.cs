using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Player player;
	public List<Skill> skillList = new List<Skill>(10);
	public List<Skill> weaponSkillList = new List<Skill>();
    public Skill skill;
	public int skillValue = 0;
	public int maxPlayerSkill = 3;
	public int curPlayerSkillMax = 0;

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
		weaponSkillList = skillList.Where(skill => skill.weaponCon != null).ToList();
		skill = weaponSkillList[0];
	}

	public void Pick()
	{
		player = GameObject.FindAnyObjectByType<Player>();
		player.playerSkill.Add(skill);
		player.weapons.Add(Instantiate(skill.weaponCon, player.transform.position, Quaternion.identity, player.transform));

		float value = skill.levelSkills[skill.level].value;


		switch (skill.levelSkills[skill.level].upgradeType)
		{
			case SkillType.ArrowSpeedUp:

				foreach (var weapon in player.weapons)
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
				foreach (var weapon in player.weapons)
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
				foreach (var weapon in player.weapons)
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
