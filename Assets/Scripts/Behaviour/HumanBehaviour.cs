using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using Interactable.Manager;
using Interaction;
using Interface;
using Manager;
using UnityEngine;

public class HumanBehaviour : Behaviour
{
	public HumanBehaviour(Responsible responsible): base(responsible)
	{
	}	
	
	public override void SetActivity()
	{
		Activity = ActivityType.Plant;
		/*
		if (Activities.Count == 0)
		{
			if (IdleActvities.Count == 0)
			{
				Activity = ActivityType.None;
				return;
			}	
				
			Activity = IdleActvities[0];
			return;
		}
			
		Activity = Activities[0];
		*/
	}
	
	public override void DoActivity()
	{
		if (Activity == ActivityType.None || JobManager.ActivityTypeExists(Responsible, Activity)) return;
		ActivityFactory.GetActivity(this);
	}

	private IEnumerator RemoveActivity(ActivityType activityType)
	{
		IdleActvities.Remove(activityType);
		Debug.Log(activityType + " removed from idle activities!");
		yield return new WaitForSeconds(150);
		IdleActvities.Add(activityType);
		Debug.Log(activityType + " added back to idle activities!");

	}
}
