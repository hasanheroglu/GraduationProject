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
	
	private MethodInfo Method { get; set; }

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
		Method = jobInfo.Method;
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
		ButtonUtil.SetOnClickAction(button, UIManager.GetJobButtonAction(this));
		Button = button;
	}

	public IEnumerator Start()
	{
		Responsible.JobFinished = false;
		Responsible.TargetInRange = false;
		
		if(Target){
			Responsible.Target = Target.gameObject;
		}
		
		if (Target == null || Target.InUse <= 0 || !Target.Methods.Contains(Method))
		{	
			Debug.Log("Target is already in use!");
			Stop(true);
			yield break;
		}
		yield return Responsible.StartCoroutine("Walk", Target.interactionPoint.transform.position);
		
		Debug.Log("Reached to the target now I'm gonna start doing my job!");
		if (Target == null || Target.InUse <= 0 || !Target.Methods.Contains(Method))
		{	
			Debug.Log("Target is already in use!");
			Stop(true);
			yield break;
		}
		Debug.Log("Nobody is using this target!");

		 
		_started = true;
		Responsible.StartCoroutine(Coroutine);
		Target.InUse--;
		
		SkillManager.AddSkill(Responsible, SkillFactory.GetSkill(SkillType));
		EffectManager.Apply(Responsible, Effects);
	}

	public void Stop(bool immediate = false)
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
		
		if (_started)
		{
			if(!immediate)
			{
				SkillManager.UpdateSkill(Responsible, SkillType, EarnedXp);
			}
			
			EffectManager.Remove(Responsible, Effects);
			_started = false;
			Target.InUse++;
		}		
		
		JobManager.RemoveJob(this);
	}
}
