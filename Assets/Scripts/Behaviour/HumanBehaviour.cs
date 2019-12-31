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
	public Responsible Responsible { get; set; }
	
	public HumanBehaviour(Responsible responsible)
	{
		Responsible = responsible;
	}	
	
	
	public void DoActivity(ActivityType activityType)
	{
		if (activityType == ActivityType.None || JobManager.ActivityTypeExists(Responsible, activityType)) return;

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

				if (activityAttribute == null || activityAttribute.ActivityType != activityType ||
				    (interactable.InUse <= 0)) continue;
				
				var coroutineInfo = new JobInfo(Responsible, interactable, method, new object[] {Responsible});
				UIManager.SetInteractionAction(Responsible.gameObject, coroutineInfo);
				return;
			}
		}

		Responsible.Activities.Remove(activityType);
		Responsible.StartCoroutine("RemoveActivity", activityType);
	}

	private IEnumerator RemoveActivity(ActivityType activityType)
	{
		Responsible.IdleActvities.Remove(activityType);
		Debug.Log(activityType + " removed from idle activities!");
		yield return new WaitForSeconds(150);
		Responsible.IdleActvities.Add(activityType);
		Debug.Log(activityType + " added back to idle activities!");

	}
}
