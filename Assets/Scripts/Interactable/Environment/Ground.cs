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
			InUse = -1; //infinite use
			SetMethods();
		}
		
		[ActivityType(ActivityType.None)]
		public IEnumerator Walk(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			human.GetComponent<Responsible>().FinishJob();
			Debug.Log("Finished walking!");
		}
	}
}