using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
{
	Fist,
	Pistol,
	Shotgun
};

public class Weapon: Interactable.Base.Interactable, IPickable, IEquippable
{
	public GameObject target;
	public Responsible Responsible { get; set; }
	
	private WeaponPattern _weaponPattern;

	private void Start()
	{ 
		InUse = 1;
		SetMethods();
		Responsible = transform.parent.parent.gameObject.GetComponent<Responsible>();
	}

	public WeaponPattern GetWeaponPattern()
	{
		return _weaponPattern;
	}

	public void SetWeaponPattern(WeaponPattern weaponPattern)
	{
		_weaponPattern = weaponPattern;
	}

	public Coroutine Use(Interactable.Base.Interactable interactable, Coroutine coroutine)
	{
		target = interactable.gameObject;
		
		if (CheckTargetInRange())
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			
			Responsible.StopWalking();
			Responsible.TargetInRange = true;
			Responsible.Turn();
			_weaponPattern.Use(this.gameObject, new Vector3(target.gameObject.transform.position.x - gameObject.transform.position.x, 0, target.gameObject.transform.position.z - gameObject.transform.position.z));
			Responsible.TargetInRange = false;
		}
		else
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			
			var circlePos = Vector3.forward * _weaponPattern.range;
			circlePos = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * circlePos;
			coroutine = StartCoroutine(Responsible.Walk(target.GetComponent<Interactable.Base.Interactable>().interactionPoint.transform.position + circlePos));
		}

		return coroutine;
	}

	public IEnumerator Reload()
	{
		yield return new WaitForSeconds(_weaponPattern.delay);
	}

	public bool CheckTargetInRange()
	{
		//Debug.DrawLine(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3(target.gameObject.transform.position.x, 0, target.gameObject.transform.position.z), Color.red, 2f);
		return Vector2.Distance(new Vector2(Responsible.gameObject.transform.position.x, Responsible.gameObject.transform.position.z),
			       new Vector2(target.gameObject.transform.position.x, target.gameObject.transform.position.z)) <=
		       _weaponPattern.range;
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
		yield return new WaitForSeconds(0.1f);
		Debug.Log( responsible.Name + " equipped the weapon.");
		responsible.Weapon = this;
		this.gameObject.transform.SetParent(responsible.weaponPosition.transform);
		this.gameObject.transform.position = responsible.weaponPosition.transform.position;
		this.gameObject.transform.rotation = responsible.gameObject.transform.rotation;
		responsible.FinishJob();
	}
/*
	private void OnTriggerEnter(Collider other)
	{
		if (_weaponPattern.range < 7.5f && other.gameObject.CompareTag("Damagable"))
		{
			Debug.Log("Hit damagable!");
			other.gameObject.GetComponentInParent<Interactable.Base.Interactable>().health -= _weaponPattern.damage;
			other.gameObject.GetComponentInParent<Rigidbody>().AddForce(Vector3.forward * 100);
		}
	}
	*/
}
