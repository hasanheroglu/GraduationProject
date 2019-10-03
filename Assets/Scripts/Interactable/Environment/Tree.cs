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

		public IEnumerator Chop(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", interactionPoint.transform.position);
			Debug.Log(human.Name + " is chopping the plant");

			var activity = ActivityFactory.GetChop();
			human.AddActivity(activity);
			
			human.AddSkill(SkillFactory.GetLumberjack());			
			yield return new WaitForSeconds(6);
			Destroy(gameObject);
			human.UpdateSkill(SkillType.Lumberjack, 400);
			
			Debug.Log("Chopped the plant by " + human.Name);
			human.GetComponent<Responsible>().FinishJob();
		}
	}
}
