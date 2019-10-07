using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Interactable.Base
{
	public abstract class Responsible : Interactable
	{
		private bool _jobFinished;

		private NavMeshAgent _agent;
		private List<Job> _jobs;
		private bool _targetInRange;
		private GameObject _target;
		private GameObject _jobPanel;
		private List<ActivityType> _activityList;
		private Dictionary<NeedType, Need> _needs;
		private Dictionary<ActivityType, Activity> _activities;
		private Dictionary<SkillType, Skill> _skills;
		private List<Interactable> _inventory;

		public List<Job> Jobs
		{
			get { return _jobs; }
			set { _jobs = value; }
		}
		
		public GameObject Target
		{
			get { return _target; }
			set { _target = value; }
		}
		
		public bool TargetInRange
		{
			get { return _targetInRange; }
			set { _targetInRange = value; }
		}

		public NavMeshAgent Agent
		{
			get { return _agent; }
			set { _agent = value; }
		}

		public bool JobFinished
		{
			get { return _jobFinished; }
			set { _jobFinished = value; }
		}

		public GameObject JobPanel
		{
			get { return _jobPanel; }
			set { _jobPanel = value; }
		}

		public List<ActivityType> ActivityList
		{
			get { return _activityList; }
			set { _activityList = value; }
		}

		public Dictionary<NeedType, Need> Needs
		{
			get { return _needs; }
			set { _needs = value; }
		}

		public Dictionary<ActivityType, Activity> Activities
		{
			get { return _activities; }
			set { _activities = value; }
		}

		public Dictionary<SkillType, Skill> Skills
		{
			get { return _skills; }
			set { _skills = value; }
		}

		public List<Interactable> Inventory
		{
			get { return _inventory; }
			set { _inventory = value; }
		}

		private void Awake()
		{
			_jobs = new List<Job>();
			_activityList = new List<ActivityType>();
			_needs = new Dictionary<NeedType, Need>();
			_activities = new Dictionary<ActivityType, Activity>();
			_skills = new Dictionary<SkillType, Skill>();
			_inventory = new List<Interactable>();
			
			_agent = GetComponent<NavMeshAgent>();
			_jobFinished = true;
			_jobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			_jobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(_jobPanel);
		}

		private void Update()
		{
			foreach (var need in _needs){ need.Value.Update(); }
			
			foreach (var activity in _activities)
			{
				foreach (var effect in activity.Value.Effects)
				{
					ApplyEffect(effect.NeedType, effect.StepValue);
				}
			}

			DoJob();
		}

		public IEnumerator Walk(Vector3 position)
		{
			_agent.isStopped = false;
			_agent.SetDestination(position);
			yield return new WaitUntil((() => _targetInRange));
			StopWalking();
		}

		public void StopWalking()
		{
			_agent.isStopped = true;
			_agent.ResetPath();
		}

		private void DoJob()
		{		
			if (_jobFinished && _jobs.Count != 0)
			{	
				_jobs[0].Start();
			}
		}

		public void StopDoingJob(Job job)
		{
			if(_activities.Count > 0)
			{
				_activities.Remove(_activityList[0]);
				_activityList.RemoveAt(0);
			}

			job.Stop();
		}

		public void FinishJob()
		{			
			if(_activities.Count > 0){
				_activities.Remove(_activityList[0]);
				_activityList.RemoveAt(0);
			}
			
			_jobs[0].Finish();
		}

		private void ApplyEffect(NeedType needType, float stepValue)
		{
			if (_needs.ContainsKey(needType)){ _needs[needType].StepValue = stepValue; }
		}
		
 		public IEnumerator ApplyEffectForSeconds(NeedType needType, float stepValue, float effectDuration)
		{
			if (_needs.ContainsKey(needType)){ _needs[needType].StepValue = stepValue; }	
			yield return new WaitForSeconds(effectDuration);
			if (_needs.ContainsKey(needType)){ _needs[needType].Reset(); }
		}

		public void AddSkill(Skill skill)
		{
			if (!_skills.ContainsKey(skill.SkillType)){ _skills.Add(skill.SkillType, skill); }
			UIManager.Instance.SetSkills(this);
		}

		public void UpdateSkill(SkillType skillType, float xp)
		{
			if (_skills.ContainsKey(skillType))
			{
				_skills[skillType].LevelUp(xp);
			}
		}

		public void AddActivity(Activity activity)
		{
			if (!_activities.ContainsKey(activity.ActivityType))
			{
				_activityList.Add(activity.ActivityType);
				_activities.Add(activity.ActivityType, activity);
			}
		}

		public void RemoveActivity(ActivityType activityType)
		{
			if (_activities.ContainsKey(activityType)){ _activities.Remove(activityType); }
		}
	}
}
