using System.Collections;
using System.Collections.Generic;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Tent : Interactable.Base.Interactable, ISleepable
	{
		private void Start()
		{
			InUse = 1;
			SetMethods();
		}
		
		[ActivityType(ActivityType.Sleep)]
		public IEnumerator Sleep(Human human)
		{
			
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log(human.Name + " is sleeping");
			
			human.AddActivity(ActivityFactory.GetSleep());
			yield return new WaitForSeconds(15);
			Debug.Log(human.Name + " slept!");
			human.GetComponent<Responsible>().FinishJob();
		}
	}
}
