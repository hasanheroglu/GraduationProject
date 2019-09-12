using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
	private Needs needType;
	private float stepValue;

	public Needs NeedType
	{
		get { return needType; }
		set { needType = value; }
	}
	
	public float StepValue
	{
		get { return stepValue; }
		set { stepValue = value; }
	}
	
	public Effect(Needs needType, float stepValue)
	{
		this.needType = needType;
		this.stepValue = stepValue;
	}
}
