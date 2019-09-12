using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Tree : Base.Interactable, IChoppable {
		public IEnumerator Chop(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is chopping the plant");
			
			var effects = new List<Effect> {new Effect(Needs.Energy, -0.6f), new Effect(Needs.Hygiene, -0.5f)};
			var activity = new Activity(ActivityType.Chop, effects);
			human.Activities.Add(activity);

			var skillExists = false;
			foreach (var skill in human.Skills)
			{
				if (skill.SkillType == Skills.Lumberjack)
				{
					skillExists = true;
				}
			}

			if (!skillExists)
			{
				var skill = new Skill(Skills.Lumberjack, 0, 0, 1000, 1.2f);
				human.Skills.Add(skill);
			}
			
			yield return new WaitForSeconds(6);
			human.Activities.Remove(activity);
			Destroy(gameObject);

			foreach (var skill in human.Skills)
			{
				if (skill.SkillType == Skills.Lumberjack)
				{
					skill.TotalXp += 1200;
				}
			}
			
			Debug.Log("Chopped the plant by " + human.Name);
			human.GetComponent<Responsible>().FinishJob();
		}
	}
}
