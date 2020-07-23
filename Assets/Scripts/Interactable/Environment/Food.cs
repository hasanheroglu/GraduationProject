using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Food : Pickable, IEdible
{
	private static int _instanceCount;

	[SerializeField] private float eatDuration;
	
	private void Awake()
	{
		instanceNo = _instanceCount;
		_instanceCount++;
		eatDuration = 3.0f;
		InUse = 1;
		SetMethods();
		base.Awake();
	}

	[Activity(ActivityType.Eat)]
	[Interactable(typeof(Human))]
	[Skill(SkillType.None)]
	public IEnumerator Eat(Responsible responsible)
	{
		if(!picked){
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		}

		yield return Util.WaitForSeconds(responsible.GetCurrentJob(), eatDuration);

		if(picked){
			responsible.Inventory.Remove(GetGroupName());
		}

		Destroy(this.gameObject, 0.1f);
		responsible.FinishJob();
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Pick(Responsible responsible)
	{
		return base.Pick(responsible);
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Drop(Responsible responsible)
	{
		return base.Drop(responsible);
	}
}
