using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attribute;
using Factory;
using Interactable.Base;
using Interactable.Manager;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class Job
{
	private bool _started;
	public Responsible Responsible { get; private set; }

	private IEnumerator Coroutine { get; set; }

	private Interactable.Base.Interactable Target { get; set; }

	public ActivityType ActivityType { get; private set; }
	
	public SkillType SkillType { get; set; }
	
	public float EarnedXp { get; set; }
	
	public List<Effect> Effects { get; set; }
		
	public GameObject Button { get; private set; }

	public Job(JobInfo jobInfo)
	{
		Responsible = jobInfo.Responsible;
		SetCoroutine(jobInfo);
		SetTarget(jobInfo);
		SetActivityType(jobInfo.Method);
		SetSkillType(jobInfo.Method);
		SetEffects();
		SetButton(jobInfo);
		_started = false;
	}

	private void SetActivityType(MemberInfo method)
	{
		ActivityAttribute activityAttribute =
			System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;

		if (activityAttribute == null)
		{
			Debug.Log("Method's activity type is missing!");
			return;
		}
		
		ActivityType = activityAttribute.ActivityType;
	}

	private void SetEffects()
	{
		Effects = new List<Effect>();
		EffectFactory.GetEffects(ActivityType, Effects);
	}

	private void SetSkillType(MemberInfo method)
	{
		SkillAttribute skillAttribute =
			System.Attribute.GetCustomAttribute(method, typeof(SkillAttribute)) as SkillAttribute;

		if (skillAttribute == null)
		{
			Debug.Log("Method's skill type is missing!");
			return;
		}
		
		SkillType = skillAttribute.SkillType;
		EarnedXp = skillAttribute.EarnedXp;
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
		UIManager.SetJobButtons(Responsible);
		ButtonUtil.SetOnClickAction(button, UIManager.GetJobButtonAction(this, button));
		Button = button;
	}

	public IEnumerator Start()
	{
		if (Target.InUse == 0)
		{
			Stop(true);
		}

		_started = true;
		Responsible.JobFinished = false;
		Responsible.TargetInRange = false;
		Target.InUse--;
		Responsible.Target = Target.gameObject;
		yield return Responsible.StartCoroutine("Walk", Target.interactionPoint.transform.position);
		SkillManager.AddSkill(Responsible, SkillFactory.GetSkill(SkillType));
		EffectManager.Apply(Responsible, Effects);
		
		Responsible.StartCoroutine(Coroutine);
	}

	public void Stop(bool immediate = false)
	{
		if (_started)
		{
			if (Responsible.Jobs.IndexOf(this) == 0)
			{
				Responsible.StopWalking();
				Responsible.JobFinished = true;
			}

			if (immediate)
			{
				Responsible.StopCoroutine(Coroutine);
			}
			else
			{
				SkillManager.UpdateSkill(Responsible, SkillType, EarnedXp);
			}
			EffectManager.Remove(Responsible, Effects);
			Target.InUse++;
			_started = false;
		}
		
		JobManager.RemoveJob(Responsible, this);
	}
}
