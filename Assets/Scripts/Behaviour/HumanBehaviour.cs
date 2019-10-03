using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using Interface;
using Manager;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
	private Human _human;
	
	
	// Use this for initialization
	void Start ()
	{
		_human = GetComponent<Human>();
	}
	
	// Update is called once per frame
	void Update () {
		SearchEdible();
		SearchSleepable();
		SearchHarvestable();
	}

	private void SearchEdible()
	{
		if (_human.Needs[NeedType.Hunger].Value < 10 && !JobUtil.ActivityTypeExists(_human, ActivityType.Eat))
		{
			foreach (var item in _human.Inventory)
			{
				if (item is IEdible)
				{
					Debug.Log("I'm hungry, let me eat something!");
					UIManager.Instance.SetInteractionAction(this.gameObject, item.GetComponent<IEdible>().Eat(_human), item);
					Debug.Log("That was delicious!");
					_human.Inventory.Remove(item);
					return;
				}
			}
			var possibleEdibles = Physics.OverlapSphere(this.transform.position, 500f);
			if (possibleEdibles.Length > 0)
			{
				foreach (var possibleEdible in possibleEdibles)
				{
					var interactable = possibleEdible.gameObject.GetComponent<Interactable.Base.Interactable>();
					if (interactable is IEdible && interactable.InUse > 0)
					{
						if(((Plant) interactable).harvested){ continue; }
						UIManager.Instance.SetInteractionAction(this.gameObject, possibleEdible.gameObject.GetComponent<IEdible>().Eat(_human), interactable);
						return;
					}
				}
			}
		}
	}

	private void SearchSleepable()
	{
		if (_human.Needs[NeedType.Energy].Value < 10 && !JobUtil.ActivityTypeExists(_human, ActivityType.Sleep))
		{
			var possibleSleepables = Physics.OverlapSphere(this.transform.position, 500f);
			if (possibleSleepables.Length > 0)
			{
				foreach (var possibleSleepable in possibleSleepables)
				{
					var interactable = possibleSleepable.gameObject.GetComponent<Interactable.Base.Interactable>();
					if (interactable is ISleepable && interactable.InUse > 0)
					{
						UIManager.Instance.SetInteractionAction(this.gameObject, possibleSleepable.gameObject.GetComponent<ISleepable>().Sleep(_human), interactable);
						break;
					}
				}
			}
		}
	}

	private void SearchHarvestable()
	{
		if (_human.JobList.Count != 0){ return; }
		var possibleHarvestables = Physics.OverlapSphere(this.transform.position, 500f);
		if (possibleHarvestables.Length > 0)
		{
			foreach (var possibleHarvestable in possibleHarvestables)
			{
				var interactable = possibleHarvestable.gameObject.GetComponent<Interactable.Base.Interactable>();
				if (interactable is IHarvestable && interactable.InUse > 0)
				{
					if(((Plant) interactable).harvested){ continue; }
					UIManager.Instance.SetInteractionAction(this.gameObject, possibleHarvestable.gameObject.GetComponent<IHarvestable>().Harvest(_human), interactable);
					break;
				}
			}
		}
	}
}
