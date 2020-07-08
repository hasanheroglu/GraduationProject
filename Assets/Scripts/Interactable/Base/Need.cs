using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public enum NeedType{Hunger, Social, Fun, Bladder, Energy, Hygiene}

public class Need
{
	private readonly NeedType _type;
	private readonly ActivityType _activity;
	private readonly float _baseStepValue;
	
	public string Name { get; set; }
	public float Value { get; set; }
	public float StepValue { get; set; }
	

	public Need(NeedType type, ActivityType activity, float baseStepValue)
	{
		_type = type;
		SelectName();
		_baseStepValue = baseStepValue;
		_activity = activity;
		StepValue = _baseStepValue;
		Value = 100f;
	}

	public void Update(Responsible responsible)
	{
		Value += StepValue;
		
		if (Value < 0){ Value = 0; }

		if (Value < 20 && !responsible.Behaviour.IsFirstActivity(_activity))
		{
			responsible.Behaviour.AddActivityToBeginning(_activity);
			responsible.Behaviour.SetActivity();
			NotificationManager.Instance.Notify(responsible.characterName + "'s " + Name + " level is critical!", responsible.gameObject.transform);
		}

		if (Value >= 20 && responsible.Behaviour.IsFirstActivity(_activity))
		{
			responsible.Behaviour.RemoveActivity(_activity);
			responsible.Behaviour.SetActivity();
		}
		
		if (Value > 100){ Value = 100; }
	}

	public void Reset()
	{
		StepValue = _baseStepValue;
	}

	private void SelectName()
	{
		switch (_type)
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
