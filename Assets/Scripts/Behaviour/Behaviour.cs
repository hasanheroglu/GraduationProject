using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Manager;
using UnityEngine;

public class Behaviour
{
	protected int activityPointer;
	public Activity Activity { get; set; }
	public List<Activity> Activities { get; set; }
	public Responsible Responsible { get; set; }

	public Behaviour(Responsible responsible)
	{
		Activities = new List<Activity>();
		activityPointer = 0;
		Responsible = responsible;
	}

	public void SetActivity()
	{
		Activity = Activities[0];
	}

	public bool IsFirstActivity(ActivityType activityType)
	{
		return Activities[0].Type == activityType;
	}
	
	public void AddActivityToBeginning(ActivityType activityType)
	{
		if(DoesContain(activityType)) RemoveActivity(activityType);
		
		var activity = ActivityFactory.GetActivity(activityType);
		if (activity == null) return;
		
		Activities.Insert(0, activity);
	}
	
	public void AddActivity(ActivityType activityType)
	{
		var activity = ActivityFactory.GetActivity(activityType);
		if (activity == null) return;
		
		Activities.Add(activity);
	}

	public void RemoveActivity(ActivityType activityType)
	{
		foreach (var activity in Activities)
		{
			if (activity.Type == activityType)
			{
				Activities.Remove(activity);
				return;
			}
		}
	}

	public bool DoesContain(ActivityType activityType)
	{
		foreach (var activity in Activities)
		{
			if (activity.Type == activityType) return true;
		}

		return false;
	}

	public void DoActivity()
	{
		if (Activity.Type == ActivityType.None || JobManager.ActivityTypeExists(Responsible, Activity.Type)) return;
		if (Activity.Do(Responsible)) return;
		
		Responsible.Wander();

		activityPointer++;
		if (activityPointer >= Activities.Count)
		{
			activityPointer = 0;
		}
			
		Activity = Activities[activityPointer];
	}
}
