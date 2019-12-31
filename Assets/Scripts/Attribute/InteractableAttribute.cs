using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class InteractableAttribute : System.Attribute
{

	private Type _interactableType;

	public InteractableAttribute(Type interactableType)
	{
		_interactableType = interactableType;
	}

	public Type InteractableType
	{
		get { return _interactableType; }
		set { _interactableType = value; }
	}
}
