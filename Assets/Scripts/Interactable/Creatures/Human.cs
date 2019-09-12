using System.Collections;
using Interactable.Base;
using Interface;
using Manager;
using UnityEngine;

namespace Interactable.Creatures
{
	public class Human : Responsible, ISocializable
	{
		public IEnumerator Talk(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " talking with " + Name);
			human.GetComponent<Responsible>().FinishJob();
		}

		public IEnumerator Attack(Human human)
		{
			yield return new WaitForSeconds(2);
			Debug.Log(human.Name + " attacked " + Name);
			human.GetComponent<Responsible>().FinishJob();
		}
	}
}
