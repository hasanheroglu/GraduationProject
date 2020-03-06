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
		base.Update();
		if (AutoWill)
		{
			Behaviour.DoActivity();
		}
	}
	
	[Interactable(typeof(Human))]
	public IEnumerator Attack(Responsible responsible)
	{
		while (health > 0)
		{
			yield return new WaitForSeconds(1f);
			if (responsible.Weapon != null)
			{
				responsible.Turn();
				responsible.Weapon.Use(this);
			}

			responsible.TargetInRange = responsible.Weapon.CheckTargetInRange();

			if (!responsible.Weapon.CheckTargetInRange())
			{
				var circlePos = Vector3.forward * responsible.Weapon.weaponPattern.range;
				circlePos = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * circlePos;
				Debug.Log("circle pos: " + circlePos);
				yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position + circlePos));
			}
			else
			{
				responsible.StopWalking();
			}
		}
		
		responsible.GetComponent<Responsible>().FinishJob();
		
		if(Jobs.Count > 0)
			StopDoingJob(Jobs[0]);		
		
		Destroy(gameObject);
	}
}
