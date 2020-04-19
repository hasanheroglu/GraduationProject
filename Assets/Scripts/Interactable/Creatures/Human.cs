using System;
using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interface;
using Manager;
using UnityEngine;

namespace Interactable.Creatures
{
	public class Human : Responsible, ISocializable, IAttackable
	{
		public void Start()
		{
			Behaviour = new HumanBehaviour(this.GetComponent<Responsible>());
			AutoWill = true;

			var weaponGo = WeaponFactory.GetShotgun(transform.position);
			weaponGo.transform.SetParent(weaponPosition.transform);
			weaponGo.transform.position = weaponPosition.transform.position;
			weaponGo.transform.rotation = gameObject.transform.rotation;

			Weapon = weaponGo.GetComponent<Weapon>();
			
			SetMethods();
			Behaviour.IdleActvities.AddRange(new []{ActivityType.Kill});
			Behaviour.SetActivity();
			InUse = 999;
		}
		
		public IEnumerator Talk(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			Debug.Log(responsible.Name + " talking with " + Name);
			responsible.GetComponent<Responsible>().FinishJob();
		}

		[Activity(ActivityType.Kill)]
		[Interactable(typeof(Zombie))]
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
}
