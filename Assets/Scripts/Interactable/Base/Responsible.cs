using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Interactable.Base
{
	public abstract class Responsible : Interactable
	{
		public List<Job> Jobs { get; set; }

		public GameObject Target { get; set; }

		public bool TargetInRange;

		public NavMeshAgent Agent { get; set; }
		
		public bool JobFinished { get; set; }
		
		public GameObject JobPanel { get; set; }
		
		public Dictionary<NeedType, Need> Needs { get; set; }
		
		public Dictionary<SkillType, Skill> Skills { get; set; }

		public List<InventoryItem> Inventory { get; set; }
		
		
		public Behaviour Behaviour { get; set; }
		
		public bool AutoWill { get; set; }
		
		public Weapon Weapon { get; set; }

		private void Awake()
		{
			Jobs = new List<Job>();
			Needs = new Dictionary<NeedType, Need>();
			Skills = new Dictionary<SkillType, Skill>();
			Inventory = new List<InventoryItem>();
			
			Agent = GetComponent<NavMeshAgent>();
			JobFinished = true;
			JobPanel = Instantiate(UIManager.Instance.jobPanelPrefab, UIManager.Instance.canvas.transform);
			JobPanel.SetActive(false);
			UIManager.Instance.JobPanels.Add(JobPanel);
		}

		public void Update()
		{
			Behaviour.SetActivity();
			foreach (var need in Needs){ need.Value.Update(this); }

			foreach (var item in Inventory)
			{
				Debug.Log(item.Item.GetComponent<Interactable>().Name + " count:" + item.Count);
			}
			
			StartCoroutine(DoJob());
		}

		public IEnumerator Walk(Vector3 position)
		{
			Agent.isStopped = false;
			Agent.SetDestination(position);
			if (Target.GetComponent<NavMeshAgent>() != null)
			{
				while (!TargetInRange)
				{
					Agent.destination = Target.transform.position;
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield return new WaitUntil((() => TargetInRange));
			StopWalking();
		}

		public void StopWalking()
		{
			Agent.isStopped = true;
			Agent.ResetPath();
		}

		private IEnumerator DoJob()
		{		
			if (JobFinished && Jobs.Count != 0)
			{	
				yield return Jobs[0].Start();
			}
		}

		public void StopDoingJob(Job job)
		{
			job.Stop(true);
		}

		public void FinishJob()
		{						
			Jobs[0].Stop();
		}
		
		public void AddToInventory(GameObject item)
		{
			foreach (var inventoryItem in Inventory)
			{
				if (inventoryItem.Item.GetComponent<Interactable>().Name == item.GetComponent<Interactable>().Name)
				{
					inventoryItem.Add(1);
					return;
				}
			}
			
			Inventory.Add(new InventoryItem(item));
		}

		public void RemoveFromInventory(GameObject item)
		{
			foreach (var inventoryItem in Inventory)
			{
				if (inventoryItem.Item.GetComponent<Interactable>().Name == item.GetComponent<Interactable>().Name)
				{
					if (inventoryItem.Count == 1)
					{
						Inventory.Remove(inventoryItem);
					}
					else
					{
						inventoryItem.Remove(1);
					}
				}
			}
			
		}
	}
}
