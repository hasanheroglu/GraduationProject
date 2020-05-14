using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotPattern : WeaponPattern
{
	
	public SingleShotPattern()
	{

	}
	
	public override void Use(GameObject weapon,  Vector3 direction)
	{
		if (!weapon.GetComponent<Weapon>().CanUse) return;
		
		var bulletPosition = weapon.GetComponent<Weapon>().bulletPoint.transform.position;
		var responsible = weapon.GetComponent<Weapon>().Responsible;
		var damage = weapon.GetComponent<Weapon>().damage;
		var range = weapon.GetComponent<Weapon>().range;
		
		var bulletGo = BulletFactory.GetBullet(bulletPosition, responsible, damage);
		bulletGo.GetComponent<Rigidbody>().velocity = direction.normalized * range;
	}
}
