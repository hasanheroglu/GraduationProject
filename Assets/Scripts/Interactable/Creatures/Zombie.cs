using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Zombie : Responsible, IAttackable {
	
	public void Start()
	{
		Behaviour = new ZombieBehaviour(this.GetComponent<Responsible>());
		AutoWill = true;
		
		//Behaviour.IdleActvities = new List<ActivityType> {ActivityType.Kill};
		Behaviour.SetActivity();
		SetMethods();
		InUse = 999;
	}

	[Activity(ActivityType.Kill)]
	[Interactable(typeof(Human))]
	public IEnumerator Attack(Responsible responsible)
	{
		Coroutine coroutine = null;
		
		while (health > 0)
		{
			if (responsible.Equipment.Weapon == null) break;

			yield return StartCoroutine(responsible.Turn());
			coroutine = responsible.Equipment.Weapon.Use(this, coroutine);
			yield return responsible.Equipment.Weapon.Reload();
		}
		
		responsible.GetComponent<Responsible>().FinishJob();
	}
	
}
