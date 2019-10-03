using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

namespace Interactable.Environment
{
	public class Plant : Interactable.Base.Interactable, IHarvestable, IEdible
	{
		public bool harvested;
		public GameObject plantMesh;

		private void Start()
		{
			InUse = 1;
			harvested = false;
			SetMethods();
		}

		public IEnumerator Harvest(Human human)
		{			
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is harvesting the plant");
			human.AddSkill(SkillFactory.GetGardening());
			yield return new WaitForSeconds(2);
			human.UpdateSkill(SkillType.Gardening, 500);
			Debug.Log("Plant harvested by " + human.Name);
			human.GetComponent<Responsible>().FinishJob();
			yield return Refresh();
		}

		[ActivityType(ActivityType.Eat)]
		public IEnumerator Eat(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is eating the plant");
			yield return new WaitForSeconds(2);
			human.GetComponent<Responsible>().FinishJob();
			Debug.Log("Plant eaten by " + human.Name);
			yield return human.ApplyEffectForSeconds(NeedType.Hunger, 	30f, 1000f);
			yield return Refresh();
		}

		private IEnumerator Refresh()
		{
			harvested = true;
			Methods.Remove(GetType().GetMethod("Harvest"));
			Methods.Remove(GetType().GetMethod("Eat"));

			if (plantMesh.activeSelf)
			{
				plantMesh.SetActive(false);
			}
			
			yield return new WaitForSeconds(60);
			harvested = false;
			
			if (!plantMesh.activeSelf)
			{
				Methods.Add(GetType().GetMethod("Harvest"));
				Methods.Add(GetType().GetMethod("Eat"));
				plantMesh.SetActive(true);
			}
		}
	}
}
