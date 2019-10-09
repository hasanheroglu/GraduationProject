using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Manager;
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

		[Activity(ActivityType.Harvest)]
		[Skill(SkillType.Gardening, 500)]
		public IEnumerator Harvest(Human human)
		{
			Debug.Log(human.Name + " is harvesting the plant");
			yield return new WaitForSeconds(2);
			Debug.Log("Plant harvested by " + human.Name);
			human.FinishJob();
			yield return Refresh();
		}

		[Activity(ActivityType.Eat)]
		[Skill(SkillType.None)]
		public IEnumerator Eat(Human human)
		{
			Debug.Log(human.Name + " is eating the plant");
			yield return new WaitForSeconds(2);
			Debug.Log("Plant eaten by " + human.Name);
			human.FinishJob();
			yield return Refresh();
		}

		private IEnumerator Refresh()
		{
			harvested = true;
			Debug.Log("Harvested:" + harvested);
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
