using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Interactable.Base
{
	public abstract class Responsible : Interactable
	{
		public List<Job> Jobs { get; set; }

		public GameObject Target { get; set; }

		public bool TargetInRange { get; set; }

		public NavMeshAgent Agent { get; set; }
		
		public ActivityType Activity { get; set; }

		public bool JobFinished { get; set; }
		
		public GameObject JobPanel { get; set; }
		
		public Dictionary<NeedType, Need> Needs { get; set; }
		
		public List<ActivityType> Activities { get; set; }
 
		public Dictionary<SkillType, Skill> Skills { get; set; }

		public List<Interactable> Inventory { get; set; }

		private void Awake()
		{
			Jobs = new List<Job>();
			Needs = new Dictionary<NeedType, Need>();
			Skills = new Dictionary<SkillType, Skill>();
			Activities = new List<ActivityType>();
			Inventory = new List<Interactable>();
			Activity = ActivityType.Chop;
			
			Agent = GetComponent<NavMeshAgent>();
			JobFinished = true;
			JobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			JobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(JobPanel);
		}

		private void Update()
		{
			Debug.Log(Activity);
			SetActivity();
			foreach (var need in Needs){ need.Value.Update(this); }
			StartCoroutine(DoJob());
		}

		public IEnumerator Walk(Vector3 position)
		{
			Agent.isStopped = false;
			Agent.SetDestination(position);
			yield return new WaitUntil((() => TargetInRange));
			StopWalking();
		}

		public void StopWalking()
		{
			Agent.isStopped = true;
			Agent.ResetPath();
		}

		private IEnumerator DoJob()
		{		
			if (JobFinished && Jobs.Count != 0)
			{	
				yield return Jobs[0].Start();
			}
		}

		public void StopDoingJob(Job job)
		{
			job.Stop(true);
		}

		public void FinishJob()
		{						
			Jobs[0].Stop();
		}

		public void SetActivity()
		{
			if (Activities.Count == 0)
			{
				Activity = ActivityType.None;
				return;
			}

			Activity = Activities[0];
		}
	}
}
