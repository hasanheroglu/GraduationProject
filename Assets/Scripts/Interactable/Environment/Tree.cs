using System.Collections;
using System.Collections.Generic;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

namespace Interactable.Environment
{
	public class Tree : Base.Interactable, IChoppable
	{	
		private void Start()
		{
			InUse = 1;
			SetMethods();
		}

		[Activity(ActivityType.Chop)]
		[Interactable(typeof(Human))]
		[Skill(SkillType.Lumberjack, 250)]
		public IEnumerator Chop(Human human)
		{
			Debug.Log(human.Name + " is chopping the plant");
			yield return new WaitForSeconds(6);
			Destroy(gameObject);			
			Debug.Log("Chopped the plant by " + human.Name);
			human.FinishJob();
		}
	}
}
