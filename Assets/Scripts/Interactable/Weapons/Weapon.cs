using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;

public class Weapon: Interactable.Base.Interactable, IPickable, IEquippable
{
	public WeaponPattern weaponPattern;

	public GameObject target;
	public GameObject bullet;


	private void Start()
	{ 
		InUse = 1;
		weaponPattern = new PistolPattern();
		SetMethods();
	}

	public void Use(Interactable.Base.Interactable interactable)
	{
		target = interactable.gameObject;
		
		if (CheckTargetInRange())
		{
			weaponPattern.Use(this.gameObject, bullet, new Vector3(target.gameObject.transform.position.x - gameObject.transform.position.x, 0, target.gameObject.transform.position.z - gameObject.transform.position.z));
			Debug.Log("Shot the weapon.");
		}
	}

	public bool CheckTargetInRange()
	{
		Debug.DrawLine(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3(target.gameObject.transform.position.x, 0, target.gameObject.transform.position.z), Color.red, 2f);
		return Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z),
			       new Vector2(target.gameObject.transform.position.x, target.gameObject.transform.position.z)) <=
		       weaponPattern.range;
	}
	
	[Interactable(typeof(Human))]
	public IEnumerator Pick(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		Debug.Log(responsible.Name + " is picking up the weapon.");
		yield return new WaitForSeconds(0.1f);
		Debug.Log("Weapon picked up by " + responsible.Name);
		responsible.Inventory.Add(this.gameObject);
		this.gameObject.transform.position = new Vector3(0f, -100f, 0f);
		responsible.FinishJob();
	}

	[Interactable(typeof(Human))]
	public IEnumerator Equip(Responsible responsible)
	{
		//yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		yield return new WaitForSeconds(0.1f);
		Debug.Log( responsible.Name + " equipped the weapon.");
		responsible.Weapon = this;
		this.gameObject.transform.SetParent(responsible.weaponPosition.transform);
		this.gameObject.transform.position = responsible.weaponPosition.transform.position;
		this.gameObject.transform.rotation = responsible.gameObject.transform.rotation;
		responsible.FinishJob();
	}
}
