using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Water : Interactable.Base.Interactable, IShowerable, IWalkable
{
	private static int _instanceCount;
	
	private void Start()
	{
		SetGroupName("water");
		instanceNo = _instanceCount;
		_instanceCount++;
		InUse = 1;
		SetMethods();
	}

	[Activity(ActivityType.Shower)]
	[Interactable(typeof(Human))]
	public IEnumerator Shower(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		responsible.GetCurrentJob().SetProgressDuration(10);
		yield return new WaitForSeconds(10);
		responsible.FinishJob();
	}

	[Interactable(typeof(Human))]
	public IEnumerator Walk(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		responsible.FinishJob();
		Debug.Log("Finished walking!");
		yield break;
	}
}
