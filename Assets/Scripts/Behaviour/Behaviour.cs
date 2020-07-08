using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Manager;
using UnityEngine;

public class Behaviour: MonoBehaviour
{
	protected int activityPointer;
	public ActivityType Activity { get; set; }
	
	[SerializeField] public List<ActivityType> activities;
	public Responsible Responsible { get; set; }

	public bool autoWill;

	private void Start()
	{
		activityPointer = 0;
		SetActivity();
		Responsible = GetComponent<Responsible>();
	}

	private void Update()
	{
		if (autoWill)
		{
			DoActivity();
		}
	}

	public Behaviour(Responsible responsible)
	{
		activityPointer = 0;
		Responsible = GetComponent<Responsible>();
	}

	public void SetActivity()
	{
		Activity = activities[0];
	}

	public bool IsFirstActivity(ActivityType activityType)
	{
		return activities[0] == activityType;
	}
	
	public void AddActivityToBeginning(ActivityType activityType)
	{
		if(DoesContain(activityType)) RemoveActivity(activityType);
		
		var activity = ActivityFactory.GetActivity(activityType);
		if (activity == null) return;
		
		activities.Insert(0, activity.Type);
	}
	
	public void AddActivity(ActivityType activityType)
	{
		var activity = ActivityFactory.GetActivity(activityType);
		if (activity == null) return;
		
		activities.Add(activity.Type);
	}

	public void RemoveActivity(ActivityType activityType)
	{
		foreach (var activity in activities)
		{
			if (activity == activityType)
			{
				activities.Remove(activity);
				return;
			}
		}
	}

	public bool DoesContain(ActivityType activityType)
	{
		foreach (var activity in activities)
		{
			if (activity == activityType) return true;
		}

		return false;
	}

	public void DoActivity()
	{
		if (Activity == ActivityType.None || JobManager.ActivityTypeExists(Responsible, Activity)) return;
		if (ActivityFactory.GetActivity(Activity).Do(Responsible)) return;
		
		Responsible.Wander();

		activityPointer++;
		if (activityPointer >= activities.Count)
		{
			activityPointer = 0;
		}
			
		Activity = activities[activityPointer];
	}
}
