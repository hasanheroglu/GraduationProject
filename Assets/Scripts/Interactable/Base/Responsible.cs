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
		private List<ActivityType> _jobActivityList;
		private List<IEnumerator> _jobList;
		private List<Interactable> _targetList;
		private bool _targetInRange;
		private GameObject _target;
		private GameObject _jobPanel;
		private List<GameObject> _buttonList;
		private List<ActivityType> _activityList;
		private Dictionary<NeedType, Need> _needs;
		private Dictionary<ActivityType, Activity> _activities;
		private Dictionary<SkillType, Skill> _skills;
		private List<Interactable> _inventory;
		
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

		public List<ActivityType> JobActivityList
		{
			get { return _jobActivityList; }
			set { _jobActivityList = value; }
		}
		
		public List<IEnumerator> JobList
		{
			get { return _jobList; }
			set { _jobList = value; }
		}

		public List<Interactable> TargetList
		{
			get { return _targetList; }
			set { _targetList = value; }
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

		public List<GameObject> ButtonList
		{
			get { return _buttonList; }
			set { _buttonList = value; }
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
			_jobActivityList = new List<ActivityType>();
			_jobList =  new List<IEnumerator>();
			_targetList = new List<Interactable>();
			_buttonList = new List<GameObject>();
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

		private void StopWalking()
		{
			_agent.isStopped = true;
			_agent.ResetPath();
		}

		private void DoJob()
		{
			if (_targetList.Count > 0  && _targetList[0] == null)
			{
				FinishJob();
			}
			
			
			if (_jobFinished && _jobList.Count != 0)
			{
				if (_targetList[0] == null)
				{
					FinishJob();
					return;
				}

				if (_targetList[0].InUse <= 0)
				{
					CancelJob();
					return;
				}
				
				_jobFinished = false;
				_targetList[0].InUse--;
				_target = _targetList[0].gameObject;
				_targetInRange = false;
			
				StartCoroutine(_jobList[0]);
			}
		}

		public void StopDoingJob(IEnumerator job)
		{
			if (!_jobList.Contains(job))
			{
				Debug.Log("Does not contain job!");
				StopCoroutine(job);
				StopWalking();
				FinishJob();
			}
			else
			{
				var jobIndex = _jobList.IndexOf(job);
				Debug.Log("I am gonna remove job with index " + jobIndex);

				if (jobIndex == 0)
				{
					StopCoroutine(_jobList[0]);
					StopWalking();
					FinishJob();
				}
				else
				{
					if(_activities.Count > 0)
					{
						_activities.Remove(_activityList[0]);
						_activityList.RemoveAt(0);
					}
					JobUtil.RemoveTarget(_targetList, jobIndex);
					JobUtil.RemoveJob(_jobList, job);
					JobUtil.RemoveButton(_buttonList, jobIndex);
				}
			}
		}

		public void FinishJob()
		{
			if (_jobList.Count == 0){ return; }
			
			_jobFinished = true;
			_jobList.RemoveAt(0);
			
			if(_activities.Count > 0){
				_activities.Remove(_activityList[0]);
				_activityList.RemoveAt(0);
			}
			
			_targetList[0].InUse++;
			_targetList.RemoveAt(0);
			JobUtil.RemoveButton(_buttonList, 0);
		}

		public void CancelJob()
		{
			if (_jobList.Count == 0){ return; }
			
			_jobFinished = true;
			_jobList.RemoveAt(0);
			
			if(_activities.Count > 0){
				_activities.Remove(_activityList[0]);
				_activityList.RemoveAt(0);
			}

			_targetList.RemoveAt(0);
			JobUtil.RemoveButton(_buttonList, 0);
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
