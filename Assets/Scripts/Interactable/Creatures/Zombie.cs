using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Zombie : Responsible
{
	private static int _instanceCount;
	
	public void Start()
	{
		SetGroupName("zombie");
		instanceNo = _instanceCount;
		_instanceCount++;
		SetMethods();
		InUse = 999;
	}

	[Activity(ActivityType.Kill)]
	[Interactable(typeof(Human))]
	public override IEnumerator Attack(Responsible responsible)
	{
		return base.Attack(responsible);
	}
	
}
