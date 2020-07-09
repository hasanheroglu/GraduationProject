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
		private static int _instanceCount;

		private bool _harvestable;
		
		[SerializeField] private float harvestDuration;
		[SerializeField] private float eatDuration;
		[SerializeField] private float growDuration;
		
		private static GameObject _product;
		private static GameObject _prefab;
		private static GameObject _seed;
		
		private void Awake()
		{
			instanceNo = _instanceCount;
			_instanceCount++;
			InUse = 1;
			_harvestable = true;
			harvestDuration = 2f;
			eatDuration = 2f;
			growDuration = 15f;
			_product = Resources.Load<GameObject>("Prefabs/Interactables/Environment/FlowerFruit");
			_prefab = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Flower");
			_seed = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Seed");
			SetMethods();
		}
		
		private IEnumerator Grow(Vector3 startScale, Vector3 endScale)
		{
			float startTime = Time.time;
			
			while (Time.time < startTime + growDuration)
			{
				transform.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime)/growDuration);
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
			responsible.animator.SetBool("isHarvesting", true);
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), harvestDuration);
			responsible.animator.SetBool("isHarvesting", false);
			GroundUtil.Clear(gameObject.transform.position);
			responsible.Inventory.Add(CreateProduct());
			Destroy(gameObject, 0.5f);
			responsible.FinishJob();
		}

		[Activity(ActivityType.Eat)]
		[Interactable(typeof(Human))]
		[Interactable(typeof(Cow))]
		[Skill(SkillType.None)]
		public IEnumerator Eat(Responsible responsible)
		{
			if (_harvestable) yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), eatDuration);
			
			if (_harvestable)
			{
				GroundUtil.Clear(gameObject.transform.position);
			}
			else
			{
				responsible.Inventory.Remove(gameObject);
			}
			
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
