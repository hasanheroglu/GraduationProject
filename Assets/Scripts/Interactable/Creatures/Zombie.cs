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
		
		var weaponGo = WeaponFactory.GetFist(transform.position);
		weaponGo.transform.SetParent(weaponPosition.transform);
		weaponGo.transform.position = weaponPosition.transform.position;
		weaponGo.transform.rotation = gameObject.transform.rotation;
		
		Weapon = weaponGo.GetComponent<Weapon>();

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
			if (responsible.Weapon == null) break;
			
			coroutine = responsible.Weapon.Use(this, coroutine);
			yield return responsible.Weapon.Reload();
		}

		yield return null;
		/*
		Coroutine coroutine = null;

		while (health > 0)
		{
			if (responsible.Weapon != null)
			{
				if (coroutine != null)
				{
					StopCoroutine(coroutine);
				}
				
				responsible.Turn();
				responsible.Weapon.Use(this);
			}

			responsible.TargetInRange = responsible.Weapon.CheckTargetInRange();

			if (!responsible.TargetInRange)
			{
				var circlePos = Vector3.forward * responsible.Weapon.GetWeaponPattern().range;
				circlePos = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * circlePos;

				if (coroutine != null)
				{
					StopCoroutine(coroutine);
				}
				coroutine = StartCoroutine(responsible.Walk(interactionPoint.transform.position + circlePos));
			}
			else
			{
				if (coroutine != null)
				{
					StopCoroutine(coroutine);
				}
				responsible.StopWalking();
			}
			
			yield return responsible.Weapon.Reload();
		}
		*/
		
		responsible.GetComponent<Responsible>().FinishJob();
		
		if(Jobs.Count > 0)
			StopDoingJob(Jobs[0]);		
		
		Destroy(gameObject);
	}
	
}
