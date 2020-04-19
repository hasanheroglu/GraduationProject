using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPattern
{
	public int damage;
	public float range;
	public float delay;

	public abstract void Use(GameObject weapon, Vector3 direction);
}
