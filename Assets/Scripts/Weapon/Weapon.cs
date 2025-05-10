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
public class Weapon : MonoBehaviour
{
	[SerializeField] protected int arrowValue;
	[SerializeField] protected float arrowSpeed;
	[SerializeField] protected float arrowDamage;
	[SerializeField] protected int itemLevel; 
	[SerializeField] protected const int MaxLevel = 3;
	//[SerializeField] protected Dictionary<int, (string, int)> levelEvent = new Dictionary<int, (string, int)>();

	public Arrow arrow ;

	//[SerializeField] protected float attackDelay = 1.0f;
	//protected float timer = 0.0f;
}
