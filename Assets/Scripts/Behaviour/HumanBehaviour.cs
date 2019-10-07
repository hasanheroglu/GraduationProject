using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using Interaction;
using Interface;
using Manager;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
	private Human _human;

	public bool activated;
	
	// Use this for initialization
	void Start ()
	{
		_human = GetComponent<Human>();
		activated = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activated)
		{
			SearchEdible();
			SearchSleepable();
			SearchHarvestable();
		}
	}

	private void SearchEdible()
	{
		if (!(_human.Needs[NeedType.Hunger].Value < 10) || JobUtil.ActivityTypeExists(_human, ActivityType.Eat)) {return;}
		var possibleEdibles = Physics.OverlapSphere(this.transform.position, 500f);
		if (possibleEdibles.Length > 0)
		{
			foreach (var possibleEdible in possibleEdibles)
			{
				var interactable = possibleEdible.gameObject.GetComponent<Interactable.Base.Interactable>();
				if (interactable is IEdible && interactable.InUse > 0)
				{
					if(((Plant) interactable).harvested){ continue; }
					var coroutineInfo = new JobInfo(_human, interactable, interactable.GetType().GetMethod("Eat"), new object[]{_human});
					UIManager.SetInteractionAction(this.gameObject, coroutineInfo);
					return;
				}
			}
		}
	}

	private void SearchSleepable()
	{
		if (!(_human.Needs[NeedType.Energy].Value < 10) ||
		    JobUtil.ActivityTypeExists(_human, ActivityType.Sleep)) {return;}
		var possibleSleepables = Physics.OverlapSphere(this.transform.position, 500f);
		if (possibleSleepables.Length > 0)
		{
			foreach (var possibleSleepable in possibleSleepables)
			{
				var interactable = possibleSleepable.gameObject.GetComponent<Interactable.Base.Interactable>();
				if (interactable is ISleepable && interactable.InUse > 0)
				{
					var coroutineInfo = new JobInfo(_human, interactable, interactable.GetType().GetMethod("Sleep"), new object[]{_human});
					UIManager.SetInteractionAction(this.gameObject, coroutineInfo);
					break;
				}
			}
		}
	}

	private void SearchHarvestable()
	{
		if (JobUtil.ActivityTypeExists(_human, ActivityType.Harvest)){ return; }
		var possibleHarvestables = Physics.OverlapSphere(this.transform.position, 500f);
		if (possibleHarvestables.Length > 0)
		{
			foreach (var possibleHarvestable in possibleHarvestables)
			{
				var interactable = possibleHarvestable.gameObject.GetComponent<Interactable.Base.Interactable>();
				if (interactable is IHarvestable && interactable.InUse > 0)
				{
					if(((Plant) interactable).harvested){ continue; }
					var coroutineInfo = new JobInfo(_human, interactable, interactable.GetType().GetMethod("Harvest"), new object[]{_human});
					UIManager.SetInteractionAction(this.gameObject, coroutineInfo);
					break;
				}
			}
		}
	}
}
