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
		private bool _harvestable;
		
		private float _harvestDuration;
		private float _eatDuration;
		private float _growDuration;
		
		private static GameObject _product;
		private static GameObject _prefab;
		private static GameObject _seed;
		
		private void Awake()
		{
			InUse = 1;
			_harvestable = true;
			_harvestDuration = 2f;
			_eatDuration = 2f;
			_growDuration = 15f;
			_prefab = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Flower");
			_seed = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Seed");
			SetMethods();
		}
		
		private IEnumerator Grow(Vector3 startScale, Vector3 endScale)
		{
			float startTime = Time.time;
			
			while (Time.time < startTime + _growDuration)
			{
				transform.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime)/_growDuration);
				yield return null;
			}

			SetHarvestable(true);			
		}
		public void SetHarvestable(bool harvestable)
		{
			if (!harvestable)
			{
				Methods.Remove(GetType().GetMethod("Harvest"));
				Methods.Remove(GetType().GetMethod("Eat"));
				transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				StartCoroutine(Grow(transform.localScale, _prefab.transform.localScale));
			}
			else
			{
				Methods.Add(GetType().GetMethod("Harvest"));
				Methods.Add(GetType().GetMethod("Eat"));
				transform.localScale = _prefab.transform.localScale;
			}
			
			_harvestable = harvestable;
		}

		
		[Activity(ActivityType.Harvest)]
		[Interactable(typeof(Human))]
		[Skill(SkillType.Gardening, 500)]
		public IEnumerator Harvest(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _harvestDuration);
			GroundUtil.Clear(gameObject.transform.position);
			responsible.Inventory.Add(this.gameObject);
			this.gameObject.transform.position = new Vector3(0f, -100f, 0f);
			responsible.FinishJob();
		}

		[Activity(ActivityType.Eat)]
		[Interactable(typeof(Human))]
		[Skill(SkillType.None)]
		public IEnumerator Eat(Responsible responsible)
		{
			if (_harvestable) yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _eatDuration);
			if (_harvestable) 	{GroundUtil.Clear(gameObject.transform.position);}
			responsible.Inventory.Remove(gameObject);
			
			responsible.Inventory.Add(CreateSeed());
			responsible.Heal(20);
			Destroy(gameObject, 0.5f);
			responsible.FinishJob();
		}

		private static GameObject CreateProduct()
		{
			var product = Instantiate(_product, Vector3.zero, Quaternion.identity);
			product.GetComponent<Pickable>().SetPicked(true);
			return product;
		}
		
		private static GameObject CreateSeed()
		{
			var seed = Instantiate(_seed, Vector3.zero, Quaternion.identity);
			seed.GetComponent<Seed>().SetPicked(true);
			seed.GetComponent<Seed>().SetProduct(_prefab);
			return seed;
		}
	}
}
