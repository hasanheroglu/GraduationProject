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
		private static int _instanceCount;
		
		public void Start()
		{
			SetGroupName("human");
			instanceNo = _instanceCount;
			_instanceCount++;
			Behaviour = new Behaviour(this.GetComponent<Responsible>());
			Behaviour.Activities = new List<Activity> {ActivityFactory.GetActivity(ActivityType.Chop), ActivityFactory.GetActivity(ActivityType.Kill)};
			Behaviour.SetActivity();
			AutoWill = false;
			SetMethods();
			InUse = 999;
		}
		
		public IEnumerator Talk(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			responsible.GetComponent<Responsible>().FinishJob();
		}

		[Activity(ActivityType.Kill)]
		[Interactable(typeof(Zombie))]
		public override IEnumerator Attack(Responsible responsible)
		{
			return base.Attack(responsible);
		}
	}
}
