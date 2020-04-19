using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolPattern : WeaponPattern
{
	public PistolPattern()
	{
		damage = 25;
		range = 25f;
		delay = 1f;
	}
	
	public override void Use(GameObject weapon,  Vector3 direction)
	{
		var bulletGo = BulletFactory.GetPistolBullet(weapon.transform.position + direction * 0.05f, damage);
		bulletGo.GetComponent<Rigidbody>().velocity = direction;
		Debug.Log("Weapon used pistol pattern!");
	}
}
