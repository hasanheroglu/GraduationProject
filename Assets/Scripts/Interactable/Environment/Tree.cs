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
		private static int _instanceCount;
		
		[SerializeField] private float chopDuration;
		private static GameObject _product;
		
		private void Start()
		{
			SetGroupName("tree");
			instanceNo = _instanceCount;
			_instanceCount++;
			chopDuration = 6f;
			InUse = 1;
			_product = Resources.Load<GameObject>("Prefabs/Interactables/Items/Wood");
			SetMethods();
		}

		[Activity(ActivityType.Chop)]
		[Interactable(typeof(Human))]
		[Skill(SkillType.Lumberjack, 250)]
		public IEnumerator Chop(Responsible responsible)
		{
			if(true) NotificationManager.Instance.Notify("You should equip an axe!", responsible.gameObject.transform);
			
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), chopDuration);
			GroundUtil.Clear(gameObject.transform.position);
			responsible.Inventory.Add(CreateProduct());
			Destroy(gameObject);
			responsible.FinishJob();
		}

		private static GameObject CreateProduct()
		{
			var wood = Instantiate(_product, new Vector3(0f, -100f, 0f), Quaternion.identity);
			wood.GetComponent<Wood>().SetPicked(true);
			return wood;
		}
	}
}
