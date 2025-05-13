using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UpgradeAbility
{
	Value,
	Speed,
	Damage,
}

[System.Serializable]
public struct Weapon
{
	public string name;
	public int arrowValue;
	public float arrowSpeed;
	public float arrowDamage;
	public float attackDelay;
	public float timer;

	//[SerializeField] protected Dictionary<int, (string, int)> levelEvent = new Dictionary<int, (string, int)>();

	//[SerializeField] protected float attackDelay = 1.0f;
	//protected float timer = 0.0f;
}
