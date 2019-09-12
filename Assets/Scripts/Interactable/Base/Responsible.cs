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
		private List<IEnumerator> _jobList;
		private List<Interactable> _targetList;
		private bool _targetInRange;
		private GameObject _target;
		private GameObject _jobPanel;
		private List<GameObject> _buttonList;
		private List<Need> _needList;
		private List<Activity> _activities;
		private List<Skill> _skills;
		
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

		public List<Need> NeedList
		{
			get { return _needList; }
			set { _needList = value; }
		}

		public List<Activity> Activities
		{
			get { return _activities; }
			set { _activities = value; }
		}

		public List<Skill> Skills
		{
			get { return _skills; }
			set { _skills = value; }
		}

		private void Start()
		{
			_jobList =  new List<IEnumerator>();
			_targetList = new List<Interactable>();
			_buttonList = new List<GameObject>();
			_needList = new List<Need>();
			_agent = GetComponent<NavMeshAgent>();
			_jobFinished = true;
			_jobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			_jobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(_jobPanel);
			
			_needList.Add(new Need(Needs.Hunger, -0.01f));
			_needList.Add(new Need(Needs.Fun, -0.05f));
			_needList.Add(new Need(Needs.Bladder, -0.012f));
			_needList.Add(new Need(Needs.Hygiene, -0.04f));
			_needList.Add(new Need(Needs.Energy, -0.03f));
			_needList.Add(new Need(Needs.Social, -0.02f));
			
			_activities = new List<Activity>();
			_skills = new List<Skill>();
		}

		private void Update()
		{
			DoJob();
			foreach (var need in _needList)
			{
				need.Update();
			}
			
			ResetNeeds();
			foreach (var activity in _activities)
			{
				foreach (var effect in activity.Effects)
				{
					ApplyEffect(effect.NeedType, effect.StepValue);
				}
			}

			foreach (var skill in _skills)
			{
				Debug.Log( skill.SkillType + " Level: " + skill.Level + " Total Xp: " + skill.TotalXp);
				if (!(skill.TotalXp > skill.NeededXp)) continue;
				
				skill.TotalXp -= skill.NeededXp;
				skill.NeededXp *= skill.NeededXpMultiplier;
				skill.Level++;
				Debug.Log(skill.SkillType + " leveled up!");
			}
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
			if (_jobFinished && _jobList.Count != 0)
			{
				_jobFinished = false;
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
					if(_activities.Count > 0){
						_activities.RemoveAt(jobIndex);
					}
					JobUtil.RemoveTarget(_targetList, jobIndex);
					JobUtil.RemoveJob(_jobList, job);
					JobUtil.RemoveButton(_buttonList, jobIndex);
				}
			}
		}

		public void FinishJob()
		{
			_jobFinished = true;
			_jobList.RemoveAt(0);
			if(_activities.Count > 0){
				_activities.RemoveAt(0);
			}			
			_targetList.RemoveAt(0);
			JobUtil.RemoveButton(_buttonList, 0);
		}

		private float ApplyEffect(Needs needType, float stepValue)
		{
			var oldStepValue = 0.0f;
			
			foreach (var need in _needList)
			{
				if (need.Id != needType) continue;

				oldStepValue = need.StepValue;
				need.StepValue = stepValue;
			}

			return oldStepValue;
		}
		
		public IEnumerator ApplyEffectForSeconds(Needs needType, float stepValue, float effectDuration)
		{
			var oldStepValue = 0.0f;
			
			foreach (var need in _needList)
			{
				if (need.Id != needType) continue;

				oldStepValue = need.StepValue;
				need.StepValue = stepValue;
			}
			
			yield return new WaitForSeconds(effectDuration);
			
			foreach (var need in _needList)
			{
				if (need.Id != needType) continue;

				need.StepValue = oldStepValue;
			}
		}

		private void ResetNeeds()
		{
			foreach (var need in _needList)
			{
				switch (need.Id)
				{
					case Needs.Hunger:
						need.StepValue = -0.01f;
						break;
					case Needs.Bladder:
						need.StepValue = -0.012f;
						break;
					case Needs.Fun:
						need.StepValue = -0.05f;
						break;
					case Needs.Hygiene:
						need.StepValue = -0.04f;
						break;
					case Needs.Energy:
						need.StepValue = -0.03f;
						break;
					case Needs.Social:
						need.StepValue = -0.02f;
						break;
				}
			}
		}
	}
}
