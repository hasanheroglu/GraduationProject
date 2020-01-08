using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class Zombie : Responsible {
	public void Start()
	{
		Behaviour = new ZombieBehaviour(this.GetComponent<Responsible>());
		AutoWill = true;
		//Behaviour.IdleActvities = new List<ActivityType> {ActivityType.Kill};
		Behaviour.SetActivity();
		SetMethods();
		InUse = 999;
	}

	public void Update()
	{
		base.Update();
		if (AutoWill)
		{
			Behaviour.DoActivity();
		}
	}
}
