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

public class HumanBehaviour : Behaviour
{
	public HumanBehaviour(Responsible responsible): base(responsible)
	{
	}	
	
	public override void SetActivity()
	{
		if (Activities.Count == 0)
		{
			if (IdleActvities.Count == 0)
			{
				Activity = ActivityType.None;
				return;
			}	
				
			Activity = IdleActvities[0];
			return;
		}
			
		Activity = Activities[0];
	}
	
	public override void DoActivity()
	{
		if (Activity == ActivityType.None || JobManager.ActivityTypeExists(Responsible, Activity)) return;

		var interactableObjects = Physics.OverlapSphere(Responsible.gameObject.transform.position, 20f);
		if (interactableObjects.Length <= 0) return;

		foreach (var interactableObject in interactableObjects)
		{
			var interactable = interactableObject.gameObject.GetComponent<Interactable.Base.Interactable>();
			if (!interactable) continue;
			var methods = interactable.Methods;
			if (methods.Count == 0) continue;
			
			foreach (var method in methods)
			{
				ActivityAttribute activityAttribute =
					System.Attribute.GetCustomAttribute(method, typeof(ActivityAttribute)) as ActivityAttribute;

				if (activityAttribute == null || activityAttribute.ActivityType != Activity ||
				    (interactable.InUse <= 0)) continue;
				
				var coroutineInfo = new JobInfo(Responsible, interactable, method, new object[] {Responsible});
				UIManager.SetInteractionAction(coroutineInfo);
				return;
			}
		}

		Activities.Remove(Activity);
		Responsible.StartCoroutine(RemoveActivity(Activity));
	}

	private IEnumerator RemoveActivity(ActivityType activityType)
	{
		IdleActvities.Remove(activityType);
		Debug.Log(activityType + " removed from idle activities!");
		yield return new WaitForSeconds(150);
		IdleActvities.Add(activityType);
		Debug.Log(activityType + " added back to idle activities!");

	}
}
