using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEngine;

namespace Interactable.Manager
{
	public class JobManager : MonoBehaviour {

		public static void AddToBeginning(Job job)
		{
			List<Job> newJobs = new List<Job> {job};

			foreach (var oldJob in job.Responsible.Jobs)
			{
				newJobs.Add(oldJob);
			}

			job.Responsible.Jobs = newJobs;
			
			if(ActionManager.Instance._responsible.Equals(job.Responsible.gameObject))
				UIManager.Instance.SetJobButtons(job.Responsible);
		}
		
		public static void AddJob(Job job)
		{
			job.Responsible.Jobs.Add(job);
			if(ActionManager.Instance._responsible.Equals(job.Responsible.gameObject))
				UIManager.Instance.SetJobButtons(job.Responsible);
		}

		public static void RemoveJob(Job job)
		{
			job.Responsible.Jobs.Remove(job);
			RemoveButton(job);
		}

		public static void RemoveButton(Job job)
		{
			ButtonUtil.Destroy(job.Button);
			if(ActionManager.Instance._responsible.Equals(job.Responsible.gameObject))
				UIManager.Instance.SetJobButtons(job.Responsible);
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
}
