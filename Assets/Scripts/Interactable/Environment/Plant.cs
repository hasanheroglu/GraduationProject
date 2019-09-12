using System.Collections;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Plant : Interactable.Base.Interactable, IHarvestable, IEdible {

		public IEnumerator Harvest(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is harvesting the plant");
			
			var skillExists = false;
			foreach (var skill in human.Skills)
			{
				if (skill.SkillType == Skills.Gardening)
				{
					skillExists = true;
				}
			}

			if (!skillExists)
			{
				var skill = new Skill(Skills.Gardening, 0, 0, 1000, 1.2f);
				human.Skills.Add(skill);
			}
			
			yield return new WaitForSeconds(2);
			Destroy(gameObject);
			
			foreach (var skill in human.Skills)
			{
				if (skill.SkillType == Skills.Gardening)
				{
					skill.TotalXp += 400;
				}
			}
			
			Debug.Log("Harvested the plant by " + human.Name);
			human.GetComponent<Responsible>().FinishJob();
		}

		public IEnumerator Eat(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is eating the plant");
			yield return new WaitForSeconds(6);
			human.GetComponent<Responsible>().FinishJob();
			Destroy(gameObject);
			Debug.Log("Ate the plant by " + human.Name);
			yield return human.ApplyEffectForSeconds(Needs.Hunger, 0.5f, 2f);
		}
	}
}
