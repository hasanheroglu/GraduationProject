using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public abstract class Behaviour
{
	public ActivityType Activity { get; set; }
	public List<ActivityType> Activities { get; set; }
	public List<ActivityType> IdleActvities { get; set; }
	public Responsible Responsible { get; set; }

	public Behaviour(Responsible responsible)
	{
		Activity = ActivityType.None;
		Activities = new List<ActivityType>();
		IdleActvities = new List<ActivityType>();
		Responsible = responsible;
	}

	public abstract void SetActivity();
	public abstract void DoActivity();
}
