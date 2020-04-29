using System.Collections;
using System.Collections.Generic;
using Interactable.Environment;
using Interactable.Manager;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Interactable.Base
{
	public abstract class Responsible : Interactable, IDamagable
	{
		private NavMeshAgent _agent;

		public List<Job> Jobs { get; set; }
		public GameObject JobPanel { get; set; }
		public bool JobFinished { get; set; }


		public GameObject Target { get; set; }
		public bool TargetInRange { get; set; }
		
		public Dictionary<NeedType, Need> Needs { get; set; }
		public Dictionary<SkillType, Skill> Skills { get; set; }

		
		public Inventory Inventory { get; set; }
		public Equipment Equipment { get; set; }
		
		
		public bool AutoWill { get; set; }
		public Behaviour Behaviour { get; set; }

		public GameObject directionPosition;
		
		
		private void Awake()
		{
			Jobs = new List<Job>();
			Needs = new Dictionary<NeedType, Need>();
			Skills = new Dictionary<SkillType, Skill>();
			Inventory = new Inventory(this);
			Equipment = new Equipment(this);

			_agent = GetComponent<NavMeshAgent>();
			JobFinished = true;
			JobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			JobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(JobPanel);
		}
		public void Update()
		{
			if (Jobs.Count > 0 && Jobs[0].Target == null) Jobs[0].Stop(true);
			
			if (health <= 0)
			{
				if(Jobs.Count > 0)
					StopDoingJob(Jobs[0]);
				Destroy(gameObject);
			}
			
			Behaviour.SetActivity();

			if (AutoWill) Behaviour.DoActivity();
			
			foreach (var need in Needs){ need.Value.Update(this); }
			
			StartCoroutine(DoJob());
		}
		

		public IEnumerator Walk(Vector3 position)
		{
			_agent.isStopped = false;
			_agent.SetDestination(position);

			if (Target.GetComponent<NavMeshAgent>() != null)
			{
				while (!TargetInRange)
				{
					_agent.destination = Target.transform.position;
					yield return new WaitForSeconds(5f);
				}
			}
			yield return new WaitUntil((() => TargetInRange));
			StopWalking();
			yield return Turn();
		}
		public void StopWalking()
		{
			_agent.isStopped = true;
			_agent.ResetPath();
			_agent.isStopped = false;
		}
		public void Wander()
		{
			if (Jobs.Count != 0) return;
		
			var position = gameObject.transform.position;
			var direction = new Vector2(position.x, position.z) + Random.insideUnitCircle * 10f;
			var destination = new Vector3(direction.x, 0.4f, direction.y);

			var ground = GroundUtil.FindGround(destination);
			if(ground != null) JobManager.AddJob(new Job(new JobInfo(this, ground, ground.GetType().GetMethod("Walk"), new object[]{this})));
		}


		public IEnumerator Turn()
		{
			var respDirection = new Vector2(directionPosition.transform.position.x - transform.position.x, directionPosition.transform.position.z - transform.position.z);
			var targetDirection = new Vector2(Target.GetComponent<Interactable>().interactionPoint.transform.position.x - transform.position.x, Target.GetComponent<Interactable>().interactionPoint.transform.position.z - transform.position.z);
			var angleBetween = Vector2.SignedAngle(respDirection.normalized, targetDirection.normalized);
			
			var newRotationEuler = transform.eulerAngles;
			newRotationEuler.y -= angleBetween;
			var newRotation = Quaternion.Euler(newRotationEuler);
			var startTime = Time.time;
			var startRotation = transform.rotation;
			var duration = 0.3f;
			
			while (Time.time < startTime + duration)
			{
				transform.rotation = Quaternion.Lerp(startRotation, newRotation, (Time.time - startTime)/duration);
				yield return null;
			}

			transform.rotation = newRotation;
		}
		
		
		public Job GetCurrentJob()
		{
			return Jobs[0];
		}
		private IEnumerator DoJob()
		{		
			if (Jobs.Count > 0 && Jobs[0].Target == null) Jobs[0].Stop(true);
			if (JobFinished && Jobs.Count != 0) yield return Jobs[0].Start();
		}
		public void StopDoingJob(Job job)
		{
			job.Stop(true);
		}
		public void FinishJob(bool immediate=false)
		{						
			Jobs[0].Stop(immediate);
		}

		
		public void Damage(int damage)
		{
			var decrease = damage * (Equipment.GetArmorValue() / 1000);
			if (decrease > damage) decrease = damage;
			
			health -= damage - (int) decrease;
		}
		public void Heal(int heal)
		{
			health += heal;

			if (health > 100) health = 100;
		}
	}
}
