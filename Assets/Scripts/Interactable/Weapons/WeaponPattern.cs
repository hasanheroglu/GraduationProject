using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPattern
{
	public float range;

	public abstract void Use(GameObject weapon, GameObject bullet, Vector3 direction);
}
