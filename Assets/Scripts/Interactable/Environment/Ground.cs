using System.Collections;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

namespace Interactable.Environment
{
	public class Ground : Interactable.Base.Interactable, IWalkable
	{
		private void Start()
		{
			InUse = 999; //infinite use change this value!!!
			SetMethods();
		}
		
		[Activity(ActivityType.None)]
		[Interactable(typeof(Responsible))]
		[Interactable(typeof(Human))]
		[Interactable(typeof(Zombie))]
		public IEnumerator Walk(Responsible responsible)
		{
			yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
			responsible.FinishJob();
			Debug.Log("Finished walking!");
			yield break;
		}
	}
}