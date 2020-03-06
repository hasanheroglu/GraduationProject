using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolPattern : WeaponPattern
{
	public PistolPattern()
	{
		range = 10f;
	}
	
	public override void Use(GameObject weapon, GameObject bullet, Vector3 direction)
	{
		var bulletGO = GameObject.Instantiate(bullet, weapon.transform.position, Quaternion.identity);
		bulletGO.GetComponent<Rigidbody>().velocity = direction;
		Debug.Log(direction);
		Debug.Log("Weapon used pistol pattern!");
	}
}
