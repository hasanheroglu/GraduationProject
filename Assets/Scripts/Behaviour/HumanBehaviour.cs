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

public class HumanBehaviour : MonoBehaviour
{
	private Human _human;
	private SphereCollider _collider;
	
	public bool activated;

	// Use this for initialization
	void Start()
	{
		_human = GetComponent<Human>();
		_collider = GetComponent<SphereCollider>();
		activated = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (activated)
		{
			DoActivity(_human.Activity);
		}
	}
	
	private void DoActivity(ActivityType activityType)
	{
		if (activityType == ActivityType.None || JobManager.ActivityTypeExists(_human, activityType)) return;

		var interactableObjects = Physics.OverlapSphere(this.transform.position, 200f);
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

				if (activityAttribute != null && activityAttribute.ActivityType == activityType && interactable.InUse > 0)
				{
					var coroutineInfo = new JobInfo(_human, interactable, method, new object[] {_human});
					UIManager.SetInteractionAction(this.gameObject, coroutineInfo);
					return;
				}
			}
		}

		_human.Activities.Remove(_human.Activity);
	}
}
