using System;
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
		public GameObject product;

		private void Start()
		{
			InUse = 1;
			harvested = false;
			SetMethods();
		}


		[Activity(ActivityType.Harvest)]
		[Interactable(typeof(Human))]
		[Skill(SkillType.Gardening, 500)]
		public IEnumerator Harvest(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			Debug.Log(responsible.Name + " is harvesting the plant");
			yield return new WaitForSeconds(2);
			Debug.Log("Plant harvested by " + responsible.Name);
			var newProduct = Instantiate(product);
			responsible.AddToInventory(newProduct);
			newProduct.SetActive(false);
			responsible.FinishJob();
			yield return Refresh();
		}

		[Activity(ActivityType.Eat)]
		[Interactable(typeof(Responsible))]
		[Interactable(typeof(Human))]
		[Skill(SkillType.None)]
		public IEnumerator Eat(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			Debug.Log(responsible.Name + " is eating the plant");
			yield return new WaitForSeconds(2);
			Debug.Log("Plant eaten by " + responsible.Name);
			responsible.FinishJob();
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
