using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
	public NeedType NeedType { get; set; }
	public float StepValue { get; set; }

	public Effect(NeedType needType, float stepValue)
	{
		this.NeedType = needType;
		this.StepValue = stepValue;
	}
}
