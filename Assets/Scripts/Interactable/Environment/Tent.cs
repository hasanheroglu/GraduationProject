using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Tent : Interactable.Base.Interactable, ISleepable
	{

		public IEnumerator Sleep(Human human)
		{
			
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is sleeping");
			
			var effects = new List<Effect> {new Effect(Needs.Energy, 0.6f)};
			var activity = new Activity(ActivityType.Sleep, effects);
			human.Activities.Add(activity);
			
			yield return new WaitForSeconds(15);
			Debug.Log(human.Name + " slept!");
			human.GetComponent<Responsible>().FinishJob();
		}
	}
}
