using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeedType{Hunger, Social, Fun, Bladder, Energy, Hygiene}

public class Need
{
	private NeedType _id;
	private string name;
	private float _value;
	private float _stepValue;
	private float _baseStepValue;

	public NeedType Id
	{
		get { return _id; }
		set { _id = value; }
	}

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public float Value
	{
		get { return _value; }
		set { _value = value; }
	}

	public float StepValue
	{
		get { return _stepValue; }
		set { _stepValue = value; }
	}

	public Need(NeedType id, float baseStepValue)
	{
		_id = id;
		SelectName();
		_baseStepValue = baseStepValue;
		_stepValue = _baseStepValue;
		_value = 100f;
	}

	public void Update()
	{
		_value += _stepValue;
		Reset();
		
		if (_value < 0){ _value = 0; }
		if (_value > 100){ _value = 100; }
	}

	public void Reset()
	{
		_stepValue = _baseStepValue;
	}

	private void SelectName()
	{
		switch (_id)
		{
			case NeedType.Hunger:
				name = "Hunger";
				break;
			case NeedType.Hygiene:
				name = "Hygiene";
				break;
			case NeedType.Fun:
				name = "Fun";
				break;
			case NeedType.Energy:
				name = "Energy";
				break;
			case NeedType.Bladder:
				name = "Bladder";
				break;
			case NeedType.Social:
				name = "Social";
				break;
			default:
				name = "Unknown";
				break;
		}
	}
}
