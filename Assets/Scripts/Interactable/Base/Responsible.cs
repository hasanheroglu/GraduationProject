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
	public abstract class Responsible : Interactable, IDamagable, IAttackable
	{
		private NavMeshAgent _agent;
		
		public Animator animator;
		public bool isPlayer;
		public bool isDead;

		public string CharacterName { get; set; }

		public List<Job> Jobs { get; set; }
		public bool JobFinished { get; set; }


		public GameObject Target { get; set; }
		public bool TargetInRange { get; set; }
		
		public Dictionary<NeedType, Need> Needs { get; set; }
		public Dictionary<SkillType, Skill> Skills { get; set; }

		
		public Inventory Inventory { get; set; }
		public Equipment Equipment { get; set; }
		
		
		public bool AutoWill { get; set; }
		public Behaviour Behaviour { get; set; }

		[SerializeField] public List<Quest> quests;

		public GameObject directionPosition;

		public void Awake()
		{
			isDead = false;
			//isPlayer = false;
			
			Jobs = new List<Job>();
			Needs = new Dictionary<NeedType, Need>();
			Skills = new Dictionary<SkillType, Skill>();
			Inventory = new Inventory(this);
			Equipment = new Equipment(this);
			quests = new List<Quest>();

			_agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			if(animator != null) animator.SetBool("isWalking", false);
			JobFinished = true;
		}
		
		public void Update()
		{
			if (isDead) return;
			if (Jobs.Count > 0 && Jobs[0].Target == null) Jobs[0].Stop(true);
			if (AutoWill) Behaviour.DoActivity();
			
			foreach (var need in Needs){ need.Value.Update(this); }
			
			StartCoroutine(DoJob());
		}
		
		public IEnumerator Walk(Vector3 position, bool followTarget = false)
		{
			_agent.isStopped = false;
			_agent.SetDestination(position);
			TargetInRange = false;
			if(animator != null) animator.SetBool("isWalking", true);

			if (followTarget && Target.GetComponent<NavMeshAgent>() != null)
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
			if(animator != null) animator.SetBool("isWalking", false);
			TargetInRange = false;
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
		
		public bool Damage(int damage)
		{
			if (isDead) return false;
			
			var decrease = damage * (Equipment.GetArmorValue() / 1000);
			if (decrease > damage) decrease = damage;
			
			health -= damage - (int) decrease;

			if (health <= 0)
			{
				health = 0;
				isDead = true;
				StopWalking();
				if(animator != null) animator.SetTrigger("dead");

				if (Jobs.Count > 0)
				{
					StopDoingJob(Jobs[0]);
				}

				Destroy(gameObject, 2.5f);
				return true;
			}

			return false;
		}
		public void Heal(int heal)
		{
			if (isDead) return;

			health += heal;

			if (health > 100) health = 100;
		}

		public virtual IEnumerator Attack(Responsible responsible)
		{
			Coroutine coroutine = null;
			Weapon weapon = responsible.Equipment.Weapon;
			weapon.SetTarget(gameObject);
			
			while (health > 0)
			{
				if (weapon == null) break;
				if (weapon.CheckTargetInRange())
				{
					if (coroutine != null)
					{
						StopCoroutine(coroutine);
						coroutine = null;
					}
					
					responsible.StopWalking();
					yield return StartCoroutine(responsible.Turn());
					
					if (!Behaviour.IsFirstActivity(ActivityType.Kill))
					{
						Behaviour.AddActivityToBeginning(ActivityType.Kill);
						Behaviour.SetActivity();
					}

					if (isPlayer)
					{
						NotificationManager.Instance.Notify(CharacterName + " is under attack!", gameObject.transform);
					}
					
					yield return weapon.Use();
				}
				
				if(coroutine == null)
				{
					coroutine = StartCoroutine(responsible.Walk(weapon.GetInRangePosition(), true));
				}

				yield return null;
			}
		
			responsible.GetComponent<Responsible>().FinishJob();
		}
	}
}
