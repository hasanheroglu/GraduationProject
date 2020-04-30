using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using Interactable.Manager;
using Interaction;
using Interface;
using Manager;
using UnityEngine;

public class ZombieBehaviour : Behaviour
{
	public ZombieBehaviour(Responsible responsible): base(responsible)
	{
	}	
	
	public override void SetActivity()
	{
		Activity = ActivityType.Kill;
	}

	public override void DoActivity()
	{
		if (Activity == ActivityType.None || JobManager.ActivityTypeExists(Responsible, Activity)) return;
		
		Responsible.Wander();
		var interactableObjects = Physics.OverlapSphere(Responsible.gameObject.transform.position, 2f);
		if (interactableObjects.Length <= 0) return;

		foreach (var interactableObject in interactableObjects)
		{
			var interactable = Util.GetInteractableFromCollider(interactableObject);
			
			if (!interactable) continue;
			if(ReferenceEquals(interactable, Responsible)) continue;

			var method = interactable.FindAllowedAction(Responsible, Activity);
			if (method == null) continue;
			
			Responsible.FinishJob();
			var coroutineInfo = new JobInfo(Responsible, interactable, method, new object[] {Responsible});
			UIManager.SetInteractionAction(coroutineInfo);
			return;
		}
	}
}
