using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotPattern : WeaponPattern
{
	
	public SingleShotPattern()
	{
		damage = 25;
		range = 10f;
		delay = 1f;
	}

	public SingleShotPattern(int damage, float range, float delay)
	{
		this.damage = damage;
		this.range = range;
		this.delay = delay;
	}
	
	public override void Use(GameObject weapon,  Vector3 direction)
	{
		var bulletPosition = weapon.GetComponent<Weapon>().bulletPoint.transform.position;
		var bulletGo = BulletFactory.GetBullet(bulletPosition, damage);
		bulletGo.GetComponent<Rigidbody>().velocity = direction.normalized * range;
	}
}
