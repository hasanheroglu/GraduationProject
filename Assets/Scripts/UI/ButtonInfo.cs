using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo
{
	private GameObject _target;
	private MethodInfo _method;
	private Interactable _interactable;
	private object[] _parameters;

	public ButtonInfo(GameObject target, MethodInfo method, Interactable interactable, object[] parameters)
	{
		_target = target;
		_method = method;
		_parameters = parameters;
		_interactable = interactable;
	}
	
	public GameObject Target
	{
		get { return _target; }
		set { _target = value; }
	}
	
	public MethodInfo Method
	{
		get { return _method; }
		set { _method = value; }
	}
	
	public object[] Parameters
	{
		get { return _parameters; }
		set { _parameters = value; }
	}
	
	public Interactable Interactable
	{
		get { return _interactable; }
		set { _interactable = value; }
	}
}
