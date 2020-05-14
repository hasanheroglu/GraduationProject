using System.Collections;
using System.Collections.Generic;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Tent : Interactable.Base.Interactable, ISleepable
	{
		private float _sleepDuration;
		private static int _instanceCount;
		
		private void Start()
		{
			SetGroupName("tent");
			instanceNo = _instanceCount;
			_instanceCount++;
			InUse = 1;
			_sleepDuration = 15f;
			SetMethods();
		}

		[Activity(ActivityType.Sleep)]
		[Interactable(typeof(Responsible))]
		[Skill(SkillType.None)]
		public IEnumerator Sleep(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _sleepDuration);
			responsible.FinishJob();
		}
	}
}
