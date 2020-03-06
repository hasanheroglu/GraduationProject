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
		var interactableObjects = Physics.OverlapSphere(Responsible.gameObject.transform.position, 1f);
		if (interactableObjects.Length <= 0) return;

		foreach (var interactableObject in interactableObjects)
		{
			var interactable = interactableObject.gameObject.GetComponent<Interactable.Base.Interactable>();
			if (!interactable)
			{
				if (interactableObject.gameObject.transform.parent.gameObject
					.GetComponent<Interactable.Base.Interactable>())
				{
					interactable = interactableObject.gameObject.transform.parent.gameObject
						.GetComponent<Interactable.Base.Interactable>();
				}
				else
				{
					continue;
				}
			}
			var methods = interactable.Methods;
			if (methods.Count == 0) continue;
			
			foreach (var method in methods)
			{
				ActivityAttribute activityAttribute =
					System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;

				if (activityAttribute == null || activityAttribute.ActivityType != Activity ||
				    (interactable.InUse <= 0)) continue;
				
				Responsible.StopWandering();
				var coroutineInfo = new JobInfo(Responsible, interactable, method, new object[] {Responsible});
				UIManager.SetInteractionAction(coroutineInfo);
				return;
			}
		}
	}
}
