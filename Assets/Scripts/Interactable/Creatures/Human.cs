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
	public class Human : Responsible, ISocializable
	{
		public void Start()
		{
			Behaviour = new HumanBehaviour(this.GetComponent<Responsible>());
			AutoWill = false;
			SetMethods();
			Behaviour.IdleActvities = new List<ActivityType> {ActivityType.Chop, ActivityType.Harvest};
			Behaviour.SetActivity();
			InUse = 999;
		}

		public void Update()
		{
			if (health <= 0)
			{
				if(Jobs.Count > 0)
					StopDoingJob(Jobs[0]);
				Destroy(gameObject);
			}
			
			base.Update();
			if (AutoWill)
			{
				Behaviour.DoActivity();
			}
		}

		public IEnumerator Talk(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			Debug.Log(responsible.Name + " talking with " + Name);
			responsible.GetComponent<Responsible>().FinishJob();
		}

		[Activity(ActivityType.Kill)]
		[Interactable(typeof(Zombie))]
		[Interactable(typeof(Human))]
		public IEnumerator Attack(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			yield return new WaitForSeconds(0.2f);
			health -= 10;
			Debug.Log(responsible.Name + " attacked " + Name);
			responsible.GetComponent<Responsible>().FinishJob();
		}
	}
}
