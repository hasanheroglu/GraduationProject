using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Needs{Hunger, Social, Fun, Bladder, Energy, Hygiene}

public class Need
{
	private Needs _id;
	private string name;
	private float _value;
	private float _stepValue;

	public Needs Id
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

	public Need(Needs id, float stepValue)
	{
		_id = id;
		SelectName();
		_stepValue = stepValue;
		_value = 100f;
	}

	public void Update()
	{
		_value += _stepValue;
/*
		if (_value < 0)
		{
			_value = 0;
		}
		
		if (_value < 30)
		{
			Debug.Log(name + " value is low!");
		}
*/
	}

	private void SelectName()
	{
		switch (_id)
		{
			case Needs.Hunger:
				name = "Hunger";
				break;
			case Needs.Hygiene:
				name = "Hygiene";
				break;
			case Needs.Fun:
				name = "Fun";
				break;
			case Needs.Energy:
				name = "Energy";
				break;
			case Needs.Bladder:
				name = "Bladder";
				break;
			case Needs.Social:
				name = "Social";
				break;
			default:
				name = "Unknown";
				break;
		}
	}
}
