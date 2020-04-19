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
		public bool harvestable;
		public bool hasGrown;
		public float growDuration;
		public GameObject plantMesh;
		public GameObject product;
		public GameObject seed;

		private void Awake()
		{
			InUse = 1;
			harvestable = true;
			hasGrown = true;
			growDuration = 15f;
			SetMethods();
		}

		private void Update()
		{
			if (!hasGrown)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * (((1.0f - 0.01f)/growDuration)/(1.0f-0.01f)));
				if (transform.localScale.x >= 1.0f - 0.01f)
				{
					hasGrown = true;
					SetStatus(true);
				}
			}
		}

		public void SetStatus(bool harvestable)
		{
			this.harvestable = harvestable;

			if (!harvestable)
			{
				hasGrown = false;
				Methods.Remove(GetType().GetMethod("Harvest"));
				Methods.Remove(GetType().GetMethod("Eat"));
				transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			}
			else
			{
				hasGrown = true;
				Methods.Add(GetType().GetMethod("Harvest"));
				Methods.Add(GetType().GetMethod("Eat"));
				transform.localScale = new Vector3(1f, 1f, 1f);
			}
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
			responsible.Inventory.Add(this.gameObject);
			this.gameObject.transform.position = new Vector3(0f, -100f, 0f);
			responsible.FinishJob();
			yield return Refresh();
		}

		[Activity(ActivityType.Eat)]
		[Interactable(typeof(Responsible))]
		[Interactable(typeof(Human))]
		[Skill(SkillType.None)]
		public IEnumerator Eat(Responsible responsible)
		{
			if (harvestable)
			{
				yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			}
			Debug.Log(responsible.Name + " is eating the plant");
			yield return new WaitForSeconds(2);
			Debug.Log("Plant eaten by " + responsible.Name);
			responsible.Inventory.Remove(Name, 1);
			var newSeed = Instantiate(seed, Vector3.zero, Quaternion.identity);
			responsible.Inventory.Add(newSeed);
			responsible.health += 20;
			Destroy(gameObject, 0.5f);
			responsible.FinishJob();
		}

		private IEnumerator Refresh()
		{
			harvestable = false;
			Methods.Remove(GetType().GetMethod("Harvest"));
			yield return null;
			//Methods.Remove(GetType().GetMethod("Eat"));
/*
			if (plantMesh.activeSelf)
			{
				plantMesh.SetActive(false);
			}
			
			yield return new WaitForSeconds(60);
			harvestable = true;
			
			if (!plantMesh.activeSelf)
			{
				Methods.Add(GetType().GetMethod("Harvest"));
				Methods.Add(GetType().GetMethod("Eat"));
				plantMesh.SetActive(true);
			}
*/
		}
	}
}
