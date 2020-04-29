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
			if(ReferenceEquals(interactable, this.Responsible)) continue;

			var methods = interactable.Methods;
			if (methods.Count == 0) continue;
			
			foreach (var method in methods)
			{
				ActivityAttribute activityAttribute =
					System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;
				
				if (activityAttribute == null || activityAttribute.ActivityType != Activity ||
				    (interactable.InUse <= 0)) continue;
				
				InteractableAttribute[] interactableAttributes =
					System.Attribute.GetCustomAttributes(method, typeof (InteractableAttribute)) as InteractableAttribute [];

				bool typeExist = false;

				if (interactableAttributes.Length <= 0) continue;
				
				foreach (var attribute in interactableAttributes)
				{
					if (attribute.InteractableType == Responsible.GetType() || attribute.InteractableType == Responsible.GetType().BaseType)
					{
						typeExist = true;
					}
				}
				
				if(!typeExist) continue;

				Responsible.StopWalking();
				var coroutineInfo = new JobInfo(Responsible, interactable, method, new object[] {Responsible});
				UIManager.SetInteractionAction(coroutineInfo);
				return;
			}
		}
	}
}
