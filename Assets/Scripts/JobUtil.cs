using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using UnityEngine;

public class JobUtil : MonoBehaviour {

	public static void AddJob(List<IEnumerator> jobList ,IEnumerator job)
	{
		jobList.Add(job);
	}

	public static void RemoveJob(List<IEnumerator> jobList, IEnumerator job)
	{
		jobList.RemoveAt(jobList.IndexOf(job));
	}

	public static void AddTarget(List<Interactable.Base.Interactable> targetList, Interactable.Base.Interactable target)
	{
		targetList.Add(target);
	}

	public static void RemoveTarget(List<Interactable.Base.Interactable> targetList, int index)
	{
		targetList.RemoveAt(index);
	}

	public static void AddButton(List<GameObject> buttonList, GameObject button)
	{
		buttonList.Add(button);
	}

	public static void RemoveButton(List<GameObject> buttonList, int index)
	{
		ButtonUtil.Destroy(buttonList[index].gameObject);
		buttonList.RemoveAt(index);
		for (var i = index; i < buttonList.Count; i++)
		{
			ButtonUtil.DropPosition(buttonList[i]);
		}
	}

	public static ActivityType GetActivityType(IEnumerator job)
	{
		Debug.Log(job.GetType());
		var objects = job.GetType().GetCustomAttributes(typeof(ActivityTypeAttribute), false);
		Debug.Log("Found " + ((ActivityTypeAttribute) objects[0]).ActivityType);
		return ((ActivityTypeAttribute) objects[0]).ActivityType;
	}

	public static bool ActivityTypeExists(Responsible responsible, ActivityType activityType)
	{
		foreach (var job in responsible.JobList)
		{
			Debug.Log("Looking for " + activityType);
			if (activityType == GetActivityType(job))
			{
				return true;
			}
		}

		return false;
	}
}
