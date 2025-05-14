using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	무기 업그레이드 열거형 
enum UpgradeAbility
{
	Value,
	Speed,
	Damage,
}

//	무기 구조체
[System.Serializable]
public struct Weapon
{
	public string name;
	public int arrowValue;
	public float arrowSpeed;
	public float arrowDamage;
	public float attackDelay;
	public float timer;
}
