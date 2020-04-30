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
			AutoWill = false;
			SetMethods();
			Behaviour.IdleActvities.AddRange(new []{ActivityType.Chop, ActivityType.Kill});
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
				if (responsible.Equipment.Weapon == null) break;
			
				yield return StartCoroutine(responsible.Turn());
				coroutine = responsible.Equipment.Weapon.Use(this, coroutine);
				yield return responsible.Equipment.Weapon.Reload();
			}

			responsible.GetComponent<Responsible>().FinishJob();
		}
	}
}
