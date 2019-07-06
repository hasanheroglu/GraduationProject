using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo
{
	private GameObject target;
	private MethodInfo method;
	private object[] parameters;

	public ButtonInfo(GameObject target, MethodInfo method, object[] parameters)
	{
		this.target = target;
		this.method = method;
		this.parameters = parameters;
	}
	
	public GameObject Target
	{
		get { return target; }
		set { target = value; }
	}
	
	public MethodInfo Method
	{
		get { return method; }
		set { method = value; }
	}
	
	public object[] Parameters
	{
		get { return parameters; }
		set { parameters = value; }
	}
}
