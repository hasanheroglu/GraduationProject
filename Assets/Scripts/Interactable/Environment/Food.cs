using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Food : Interactable.Base.Interactable, IEdible
{
	private static int _instanceCount;
	
	private void Start()
	{
		SetGroupName("food");
		instanceNo = _instanceCount;
		_instanceCount++;
		InUse = 1;
		SetMethods();
	}

	[Activity(ActivityType.Eat)]
	[Interactable(typeof(Responsible))]
	[Interactable(typeof(Human))]
	[Skill(SkillType.None)]
	public IEnumerator Eat(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		Debug.Log(responsible.name + "is eating food!");
		responsible.GetCurrentJob().SetProgressDuration(3);
		yield return new WaitForSeconds(3f);
		Debug.Log(responsible.name + " ate food!");
		Destroy(this.gameObject);
		responsible.FinishJob();
	}
}
