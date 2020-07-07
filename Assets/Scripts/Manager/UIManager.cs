using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Interactable.Base;
using Interactable.Manager;
using Interaction;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Manager
{
	public class UIManager : MonoBehaviour
	{
		private GameObject _interactionPanel;

		public GameObject canvas;
	
		[Header("Interaction")]
		public GameObject interactionButtonPrefab;
		public GameObject interactionPanelPrefab;
		[Header("Job")] 
		public GameObject jobButtonPrefab;
		public GameObject jobPanel;
		[Header("Character Info")]
		public GameObject responsibleName;
		[Header("Need Info")] 
		public GameObject needPrefab;
		public GameObject needParent;
		[Header("Skill Info")] 
		public GameObject skillPrefab;
		public GameObject skillParent;
		[Header("Inventory Info")] 
		public GameObject inventoryItemPrefab;
		public GameObject inventoryParent;
		public GameObject inventory;
		[Header("Quest Info")] 
		public GameObject questParent;
		public GameObject questTextPrefab;
		
		public static UIManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
		}

		/*
		 * CREATING INTERACTION MENU AND BUTTON
		 */
		
		private void CreateInteractionPanel(Vector3 panelPosition)
		{
			if(!_interactionPanel != null){ Destroy(_interactionPanel);}
			_interactionPanel =  Instantiate(interactionPanelPrefab, panelPosition, Quaternion.identity, canvas.transform);
		}

		public void SetInteractionPanel(Responsible responsible, Interactable.Base.Interactable target, object[] parameters, Vector3 panelPosition)
		{
			CreateInteractionPanel(panelPosition);
			
			HashSet<MethodInfo> methods = target.GetComponent<Interactable.Base.Interactable>().Methods;
			Debug.Log(methods.Count);
			
			foreach (var method in methods)
			{
				InteractableAttribute [] interactableAttributes =
					System.Attribute.GetCustomAttributes(method, typeof(InteractableAttribute)) as InteractableAttribute[];

			
				
				if (interactableAttributes == null)
				{
					Debug.Log("Method's interactable type is missing!");
					return;
				}

				foreach (var interactableAttribute in interactableAttributes)
				{
					if (interactableAttribute.InteractableType == responsible.GetType() || interactableAttribute.InteractableType == responsible.GetType().BaseType)
					{
						JobInfo jobInfo = new JobInfo(responsible, target, method, parameters);
						AddInteractionButton(jobInfo);
					}
				}
			}
		}
		
		public void CloseInteractionPanel()
		{
			if (_interactionPanel != null)
			{
				Destroy(_interactionPanel);
			}
		}
	
		private GameObject CreateInteractionButton(GameObject panel, JobInfo jobInfo)
		{
			var button = Instantiate(interactionButtonPrefab, panel.transform);
			ButtonUtil.AdjustPosition(button, -1);
			ButtonUtil.SetText(button, jobInfo.Method.Name);
			return button;
		}

		private void AddInteractionButton(JobInfo jobInfo)
		{
			var button = CreateInteractionButton(_interactionPanel, jobInfo);
			ButtonUtil.SetOnClickAction(button, GetInteractionAction(jobInfo));
		}
		
		private UnityAction GetInteractionAction(JobInfo jobInfo)
		{			
			return delegate {
				CloseInteractionPanel();
				JobManager.AddJob(new Job(jobInfo));
			};
		}

		public static void SetInteractionAction(JobInfo jobInfo, bool beginning = false)
		{
			if (beginning)
			{
				JobManager.AddToBeginning(new Job(jobInfo));
				return;
			}
			
			JobManager.AddJob(new Job(jobInfo));
		}
		
		/*
		 * CREATING JOB PANEL AND BUTTONS
		 */
		
		public GameObject GetJobButton()
		{
			var button = Instantiate(jobButtonPrefab, jobPanel.transform);
			button.transform.SetParent(null);
			return button;
		}
		
		public static UnityAction GetJobButtonAction(Job job)
		{
			return delegate {
				job.Responsible.StopDoingJob(job);
				JobManager.RemoveButton(job);
			};
		}

		public void SetJobButtons(Responsible responsible)
		{
			foreach (Transform child in jobPanel.transform)
			{
				child.SetParent(null);
			}
			
			var i = 0;
			foreach (var job in responsible.Jobs)
			{
				job.Button.transform.SetParent(jobPanel.transform);
				ButtonUtil.AdjustPosition(job.Button, 1, i);
				i++;
			}
		}
		
		/*
		 * INFO PANEL SETTINGS
		 */

		public void SetInfoPanel(Responsible responsible)
		{
			responsibleName.GetComponent<Text>().text = responsible.CharacterName;
			SetNeeds(responsible);
			SetSkills(responsible);
		}

		private void SetNeeds(Responsible responsible)
		{			
			foreach (Transform child in needParent.transform)
			{
				Destroy(child.gameObject);
			}

			var i = 0;
			foreach (var need in responsible.Needs)
			{
				var newNeed = Instantiate(needPrefab, Vector3.zero, Quaternion.identity, needParent.transform);
				newNeed.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, -20f * i, 0f);
				newNeed.GetComponent<NeedBox>().SetNeed(need.Value);
				i++;
			}
		}

		public void SetSkills(Responsible responsible)
		{			
			foreach (Transform child in skillParent.transform)
			{
				Destroy(child.gameObject);
			}

			var i = 0;
			foreach (var skill in responsible.Skills)
			{
				var newSkill = Instantiate(skillPrefab, Vector3.zero, Quaternion.identity, skillParent.transform);
				newSkill.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f,  36f-20f * i, 0f);
				newSkill.GetComponent<SkillInfo>().SetSkill(skill.Value);
				i++;
			}
		}
		
		/*
		 * INVENTORY ACTIONS
		 */

		public void SetInventory(Responsible responsible)
		{
			foreach (Transform child in inventoryParent.transform)
			{
				Destroy(child.gameObject);
			}

			Dictionary<string, int> inventoryItems = new Dictionary<string, int>();
			foreach (var item in responsible.Inventory.Items)
			{
				var itemName = item.GetComponent<Interactable.Base.Interactable>().GetGroupName();
				
				if (inventoryItems.ContainsKey(itemName)) inventoryItems[itemName]++;
				else inventoryItems.Add(itemName, 1);
			}
			
			var i = 0;
			foreach (var item in inventoryItems)
			{
				var newItem = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity,
					inventoryParent.transform);
				newItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -i*inventoryItemPrefab.GetComponent<RectTransform>().rect.height, 0f);

				Equipable equipable = responsible.Inventory.Find(item.Key).GetComponent<Equipable>();
				
				if (equipable && equipable.equipped)
				{
					newItem.GetComponent<InventoryItemInfo>().SetItemInfo("(Equipped) " + item.Key, responsible.Inventory.FindCount(item.Key));
				}
				else
				{
					newItem.GetComponent<InventoryItemInfo>().SetItemInfo(item.Key, responsible.Inventory.FindCount(item.Key));
				}
				
				newItem.GetComponent<InventoryItemInfo>().SetActionButton(delegate
				{
					UIManager.Instance.SetInteractionPanel(responsible, responsible.Inventory.Find(item.Key).GetComponent<Interactable.Base.Interactable>(), new object[] {responsible.GetComponent<Responsible>()}, Input.mousePosition);
				});
				i++;
			}
		}
		
		public void ToggleInventory()
		{
			if (ActionManager.Instance._responsible == null)
			{
				return;
			}
			
			inventory.SetActive(!inventory.activeSelf);
		}
		
		/*
		 * QUEST ACTIONS
		 */

		public void SetQuests(Responsible responsible)
		{
			foreach (Transform child in questParent.transform)
			{
				Destroy(child.gameObject);
			}

			float cumulativeHeight = -20f;
			var baseHeight = 20f;
			float height;
			var i = 0;
			
			foreach (var quest in responsible.quests)
			{
				var text = quest.description + " " + quest.doneCount + "/" + quest.repetitionCount;
				if (quest.completed) text +=  " Completed!";
				height = baseHeight * (text.Length / 20 + 1);
				
				var newQuest = Instantiate(questTextPrefab, Vector3.zero, Quaternion.identity, questParent.transform);
				var newQuestTransform = newQuest.GetComponent<RectTransform>();
				newQuestTransform.sizeDelta = new Vector2(newQuestTransform.rect.width, height);
				newQuest.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, cumulativeHeight, 0f);
				newQuest.GetComponent<Text>().text = text;
				
				cumulativeHeight -= height;
				i++;
			}
		}
	}
}
