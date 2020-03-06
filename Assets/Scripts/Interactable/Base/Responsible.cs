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
		
		public bool JobFinished { get; set; }
		
		public bool Wandering { get; set; }
		
		public Vector2 WanderPosition { get; set; }
		
		public GameObject JobPanel { get; set; }
		
		public Dictionary<NeedType, Need> Needs { get; set; }
		
		public Dictionary<SkillType, Skill> Skills { get; set; }

		public Inventory Inventory { get; set; }
		
		public Behaviour Behaviour { get; set; }
		
		public bool AutoWill { get; set; }
		
		public Weapon Weapon { get; set; }

		public GameObject weaponPosition;

		public GameObject directionPosition;

		private void Awake()
		{
			Jobs = new List<Job>();
			Needs = new Dictionary<NeedType, Need>();
			Skills = new Dictionary<SkillType, Skill>();
			Inventory = new Inventory(this);
			
			Agent = GetComponent<NavMeshAgent>();
			JobFinished = true;
			JobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			JobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(JobPanel);
		}

		public void Update()
		{
			Behaviour.SetActivity();
			foreach (var need in Needs){ need.Value.Update(this); }
			
			Debug.DrawRay(transform.position, new Vector3(directionPosition.transform.position.x - transform.position.x, 0, directionPosition.transform.position.z - transform.position.z));

			if (Target != null)
			{
				Debug.DrawRay(transform.position, new Vector3(Target.GetComponent<Interactable>().interactionPoint.transform.position.x - transform.position.x, 0, Target.GetComponent<Interactable>().interactionPoint.transform.position.z - transform.position.z));
			}
			
			StartCoroutine(DoJob());
		}

		public IEnumerator Walk(Vector3 position)
		{
			Agent.isStopped = false;
			Agent.SetDestination(position);

			if (Target.GetComponent<NavMeshAgent>() != null)
			{
				while (!TargetInRange)
				{
					Agent.destination = Target.transform.position;
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield return new WaitUntil((() => TargetInRange));
			StopWalking();
		}

		public void Turn()
		{
			var respDirection = new Vector2(directionPosition.transform.position.x - transform.position.x, directionPosition.transform.position.z - transform.position.z);
			var targetDirection = new Vector2(Target.GetComponent<Interactable>().interactionPoint.transform.position.x - transform.position.x, Target.GetComponent<Interactable>().interactionPoint.transform.position.z - transform.position.z);
			var angleBetween = Vector2.Angle(respDirection.normalized, targetDirection.normalized);
			Debug.Log("Angle: " + angleBetween);
			var newRotationEuler = transform.eulerAngles;
			newRotationEuler.y -= angleBetween;
			var newRotation = Quaternion.Euler(newRotationEuler);
			var timer = 0f;
			
			while (timer < 5f)
			{
				timer += Time.deltaTime;
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5);
			}
			
			Debug.Log(transform.rotation);
			Debug.Log(newRotation);
		}

		public void Wander()
		{
			if (transform.position.x == WanderPosition.x && transform.position.z == WanderPosition.y) 
			{
				Debug.Log("Wander position reached!");
				Wandering = false;
			}
			
			if (Wandering)
			{
				return;
			}
			
			Agent.isStopped = false;
			WanderPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
			Vector3 destination = new Vector3(WanderPosition.x, 0.4f, WanderPosition.y);
			Agent.SetDestination(destination);
			Wandering = true;
		}

		public void StopWandering()
		{
			Wandering = false;
			StopWalking();
		}

		public void StopWalking()
		{
			Agent.isStopped = true;
			Agent.ResetPath();
		}

		private IEnumerator DoJob()
		{		
			if (Jobs.Count > 0 && Jobs[0].Target == null)
			{
				Jobs[0].Stop(true);
			}
			
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
	}
}
