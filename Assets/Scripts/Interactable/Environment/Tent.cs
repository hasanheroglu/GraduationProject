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
		private void Start()
		{
			InUse = 1;
			SetMethods();
		}
		
		[Activity(ActivityType.Sleep)]
		[Skill(SkillType.None)]
		public IEnumerator Sleep(Human human)
		{
			Debug.Log(human.Name + " is sleeping");
			yield return new WaitForSeconds(15);
			Debug.Log(human.Name + " slept!");
			human.FinishJob();
		}
	}
}
