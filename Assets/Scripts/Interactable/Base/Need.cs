using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public enum NeedType{Hunger, Social, Fun, Bladder, Energy, Hygiene}

public class Need
{
	public NeedType Id { get; set; }

	public string Name { get; set; }

	public float Value { get; set; }

	public ActivityType Activity { get; set; }
	
	public float BaseStepValue { get; set; }

	public float StepValue { get; set; }

	public Need(NeedType id, ActivityType activity, float baseStepValue)
	{
		Id = id;
		SelectName();
		BaseStepValue = baseStepValue;
		Activity = activity;
		StepValue = BaseStepValue;
		Value = 100f;
	}

	public void Update(Responsible responsible)
	{
		Value += StepValue;
		
		if (Value < 0){ Value = 0; }

		if (Value < 20 && !responsible.Behaviour.Activities.Contains(Activity))
		{
			responsible.Behaviour.Activities.Add(Activity);
		}

		if (Value >= 20 && responsible.Behaviour.Activities.Contains(Activity))
		{
			responsible.Behaviour.Activities.Remove(Activity);
		}
		
		if (Value > 100){ Value = 100; }
	}

	public void Reset()
	{
		StepValue = BaseStepValue;
	}

	private void SelectName()
	{
		switch (Id)
		{
			case NeedType.Hunger:
				Name = "Hunger";
				break;
			case NeedType.Hygiene:
				Name = "Hygiene";
				break;
			case NeedType.Fun:
				Name = "Fun";
				break;
			case NeedType.Energy:
				Name = "Energy";
				break;
			case NeedType.Bladder:
				Name = "Bladder";
				break;
			case NeedType.Social:
				Name = "Social";
				break;
			default:
				Name = "Unknown";
				break;
		}
	}
}
