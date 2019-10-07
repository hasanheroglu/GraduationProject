using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using Interactable.Base;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class Job
{
	private Responsible _responsible;
	private IEnumerator _coroutine;
	private Interactable.Base.Interactable _target;
	private ActivityType _activityType;
	private GameObject _button;
	private bool _started;
	public Responsible Responsible
	{
		get { return _responsible; }
		set { _responsible = value; }
	}
	public IEnumerator Coroutine
	{
		get { return _coroutine; }
		set { _coroutine = value; }
	}

	public Interactable.Base.Interactable Target
	{
		get { return _target; }
		set { _target = value; }
	}

	public ActivityType ActivityType
	{
		get { return _activityType; }
		set { _activityType = value; }
	}

	public GameObject Button
	{
		get { return _button; }
		set { _button = value; }
	}

	public Job(JobInfo jobInfo)
	{
		_responsible = jobInfo.Responsible;
		SetCoroutine(jobInfo);
		SetTarget(jobInfo);
		SetActivityType(jobInfo.Method);
		SetButton(jobInfo);
		_started = false;
	}

	private void SetActivityType(MemberInfo method)
	{
		ActivityTypeAttribute activityTypeAttribute =
			System.Attribute.GetCustomAttribute(method, typeof(ActivityTypeAttribute)) as ActivityTypeAttribute;

		if (activityTypeAttribute == null)
		{
			Debug.Log("Method's activity type is missing!");
			return;
		}
		
		ActivityType = activityTypeAttribute.ActivityType;
	}

	private void SetTarget(JobInfo jobInfo)
	{
		Target = jobInfo.Target.GetComponent<Interactable.Base.Interactable>();
	}
	
	private void SetCoroutine(JobInfo jobInfo)
	{
		IEnumerator coroutine = null;
		try
		{
			coroutine = (IEnumerator) jobInfo.Method.Invoke(jobInfo.Target.GetComponent<Interactable.Base.Interactable>(),
				jobInfo.Parameters);
		}
		catch (ArgumentException e)
		{
			Debug.Log("Argument Exception");
		}
		catch (TargetParameterCountException e)
		{
			coroutine = (IEnumerator) jobInfo.Method.Invoke(jobInfo.Target.GetComponent<Interactable.Base.Interactable>(), null);
		}

		Coroutine = coroutine;
	}
	
	private void SetButton(JobInfo jobInfo)
	{
		var button = UIManager.Instance.GetJobButton(jobInfo.Responsible.JobPanel.transform);
		button.GetComponentInChildren<Text>().text = jobInfo.Method.Name;
		ButtonUtil.AdjustPosition(button, 1);
		ButtonUtil.SetOnClickAction(button, UIManager.GetJobButtonAction(this, button));
		_button = button;
	}

	public bool Start()
	{
		Debug.Log("Job started!");
		if (_target.InUse == 0)
		{
			Stop();
			return false; 
		}
		
		_responsible.JobFinished = false;
		_responsible.TargetInRange = false;
		_target.InUse--;
		_responsible.Target = _target.gameObject;
		_responsible.StartCoroutine(_coroutine);
		_started = true;
		
		return true;

	}
	
	public void Stop()
	{
		Debug.Log("Job stopped!");
		if (_started)
		{
			if (_responsible.Jobs.IndexOf(this) == 0)
			{
				_responsible.StopWalking();
				_responsible.JobFinished = true;
			}
			
			_responsible.StopCoroutine(_coroutine);
			_target.InUse++;
			_started = false;
		}
		
		JobUtil.RemoveJob(_responsible, this);
	}

	public void Finish()
	{
		Debug.Log("Job finished!");
		if (_started)
		{
			if (_responsible.Jobs.IndexOf(this) == 0)
			{
				_responsible.StopWalking();
				_responsible.JobFinished = true;
			}
			
			_target.InUse++;
			_started = false;
		}
		
		JobUtil.RemoveJob(_responsible, this);
	}
}
