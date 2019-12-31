using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Food : Interactable.Base.Interactable, IEdible {
	private void Start()
	{
		InUse = 1;
		SetMethods();
		Name = "Kebab";
	}

	[Activity(ActivityType.Eat)]
	[Interactable(typeof(Responsible))]
	[Interactable(typeof(Human))]
	[Skill(SkillType.None)]
	public IEnumerator Eat(Human human)
	{
		Debug.Log(human.name + "is eating food!");
		yield return new WaitForSeconds(3f);
		Debug.Log(human.name + " ate food!");
		Destroy(this.gameObject);
		human.FinishJob();
	}
}
