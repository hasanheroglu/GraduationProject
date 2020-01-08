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
		private int health = 100;
		public void Start()
		{
			Behaviour = new HumanBehaviour(this.GetComponent<Responsible>());
			AutoWill = true;
			SetMethods();
			Behaviour.IdleActvities = new List<ActivityType> {ActivityType.Chop, ActivityType.Harvest};
			Behaviour.SetActivity();
			InUse = 999;
		}

		public void Update()
		{
			if (health <= 0)
			{
				StopDoingJob(Jobs[0]);
				Destroy(gameObject);
			}
			
			base.Update();
			if (AutoWill)
			{
				Behaviour.DoActivity();
			}
		}

		public IEnumerator Talk(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " talking with " + Name);
			human.GetComponent<Responsible>().FinishJob();
		}

		[Activity(ActivityType.Kill)]
		[Interactable(typeof(Zombie))]
		[Interactable(typeof(Human))]
		public IEnumerator Attack(Responsible responsible)
		{
			yield return new WaitForSeconds(0.2f);
			health -= 10;
			Debug.Log(responsible.Name + " attacked " + Name);
			responsible.GetComponent<Responsible>().FinishJob();
		}
	}
}
