using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Interactable.Base;
using UnityEngine;
using UnityEngine.UI;

public class JobInfo
{
	private Responsible _responsible;
	private Interactable.Base.Interactable _target;
	private MethodInfo _method;
	private object[] _parameters;

	public JobInfo(Responsible responsible, Interactable.Base.Interactable target, MethodInfo method, object[] parameters)
	{
		_responsible = responsible;
		_target = target;
		_method = method;
		_parameters = parameters;
	}

	public Responsible Responsible
	{
		get { return _responsible; }
		set { _responsible = value; }
	}
	
	public Interactable.Base.Interactable Target
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
}
