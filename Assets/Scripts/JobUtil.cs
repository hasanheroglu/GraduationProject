using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using Interactable.Base;
using UnityEngine;

public class JobUtil : MonoBehaviour {

	public static void AddJob(Responsible responsible, Job job)
	{
		responsible.Jobs.Add(job);
	}

	public static void RemoveJob(Responsible responsible, Job job)
	{
		RemoveButton(responsible, job);
		responsible.Jobs.Remove(job);
	}

	private static void RemoveButton(Responsible responsible, Job job)
	{
		var index = responsible.Jobs.IndexOf(job);
		ButtonUtil.Destroy(job.Button);
		for (var i = index; i < responsible.Jobs.Count; i++)
		{
			ButtonUtil.DropPosition(responsible.Jobs[i].Button);
		}
	}

	public static bool ActivityTypeExists(Responsible responsible, ActivityType activityType)
	{
		foreach (var job in responsible.Jobs)
		{
			if (activityType == job.ActivityType){ return true; }
		}

		return false;
	}
}
