using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

public class Zombie : Responsible, IAttackable {
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
		if (health <= 0)
		{
			if(Jobs.Count > 0)
				StopDoingJob(Jobs[0]);			
			Destroy(gameObject);
		}
		base.Update();
		if (AutoWill)
		{
			Behaviour.DoActivity();
		}
	}
	
	[Interactable(typeof(Human))]
	public IEnumerator Attack(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		while (health > 0)
		{
			yield return new WaitForSeconds(0.2f);
			health -= 50;
			if (responsible.Weapon != null)
			{
				responsible.Weapon.Use();
			}
			Debug.Log(responsible.Name + " attacked " + Name);
		}
		
		responsible.GetComponent<Responsible>().FinishJob();
	}
}
