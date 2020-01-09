using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;

public class Weapon: Interactable.Base.Interactable, IPickable
{
	private WeaponPattern _weaponPattern;


	private void Start()
	{
		SetMethods();
		InUse = 1;
		_weaponPattern = new PistolPattern();
	}

	public void Use()
	{
		_weaponPattern.Use();
		Debug.Log("Shot the weapon.");
	}
	
	[Interactable(typeof(Human))]
	public IEnumerator Pick(Responsible responsible)
	{
		yield return new WaitForSeconds(0.1f);
		Debug.Log( responsible.Name + " picked up the weapon.");
		responsible.Weapon = this;
		responsible.FinishJob();
	}
}
