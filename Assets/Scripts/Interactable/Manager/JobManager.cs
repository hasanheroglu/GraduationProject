using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

namespace Interactable.Manager
{
	public class JobManager : MonoBehaviour {

		public static void AddToBeginning(Responsible responsible, Job job)
		{
			List<Job> newJobs = new List<Job> {job};

			foreach (var oldJob in responsible.Jobs)
			{
				newJobs.Add(oldJob);
			}

			responsible.Jobs = newJobs;
		}
		
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
}
