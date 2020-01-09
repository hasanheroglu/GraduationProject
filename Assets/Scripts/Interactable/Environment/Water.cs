using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Water : Interactable.Base.Interactable, IShowerable, IWalkable {
	private void Start()
	{
		InUse = 1;
		SetMethods();
	}

	[Activity(ActivityType.Shower)]
	[Interactable(typeof(Human))]
	public IEnumerator Shower(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		Debug.Log(responsible.Name + " is taking a shower!");
		yield return new WaitForSeconds(10);
		Debug.Log(responsible.Name + " took a shower!");
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
