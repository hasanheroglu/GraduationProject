using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Interactable.Base;
using Interaction;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Manager
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager _instance;
		private GameObject _interactionPanel;
		private HashSet<GameObject> _jobPanels;
		

		
		public GameObject canvas;
	
		[Header("Interaction")]
		public GameObject interactionButtonPrefab;
		public GameObject interactionPanelPrefab;
		[Header("Job")] 
		public GameObject jobButtonPrefab;
		public GameObject jobPanelPrefab;
		[Header("Character Info")]
		public GameObject responsibleName;
		[Header("Need")] 
		public GameObject needPrefab;
		public GameObject needParent;


		public HashSet<GameObject> JobPanels
		{
			get { return _jobPanels; }
			set { _jobPanels = value; }
		}

		public static UIManager Instance { get { return _instance; } }

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				_instance = this;
			}

			_jobPanels = new HashSet<GameObject>();
		}

		/*
		 * CREATING INTERACTION MENU AND BUTTON
		 */
		
		private void CreateInteractionPanel(Vector3 panelPosition)
		{
			_interactionPanel =  Instantiate(interactionPanelPrefab, panelPosition, Quaternion.identity, canvas.transform);
		}

		public void SetInteractionPanel(GameObject responsible, List<MethodInfo> methods, GameObject target, object[] parameters, Vector3 panelPosition)
		{
			CreateInteractionPanel(panelPosition);
			var script = target.GetComponent<Interactable.Base.Interactable>();

			foreach (var method in methods)
			{
				ButtonInfo buttonInfo = new ButtonInfo(target, method, script, parameters);
				AddInteractionButton(responsible, buttonInfo);
			}
		}
		
		public void CloseInteractionPanel()
		{
			if (_interactionPanel != null)
			{
				Destroy(_interactionPanel);
			}
		}
		
		private void AddInteractionButton(GameObject responsible, ButtonInfo buttonInfo)
		{
			IEnumerator enumerator = InteractionUtil.CreateCoroutine(buttonInfo);

			if (enumerator == null) return;

			var button = CreateInteractionButton(_interactionPanel, buttonInfo);
			ButtonUtil.SetOnClickAction(button, GetInteractionAction(responsible, enumerator, buttonInfo.Target.GetComponent<Interactable.Base.Interactable>(), button));
		}
		
		private GameObject CreateInteractionButton(GameObject panel, ButtonInfo buttonInfo)
		{
			var button = Instantiate(interactionButtonPrefab, panel.transform);
			
			ButtonUtil.AdjustPosition(button, -1);
			ButtonUtil.SetText(button, buttonInfo.Method.Name);
			return button;
		}

		private UnityAction GetInteractionAction(GameObject responsible, IEnumerator enumerator, Interactable.Base.Interactable target, GameObject button)
		{
			return delegate {
				CloseInteractionPanel();
				JobUtil.AddJob(responsible.GetComponent<Responsible>().JobList, enumerator);
				JobUtil.AddTarget(responsible.GetComponent<Responsible>().TargetList, target);
				AddJobButton(responsible, enumerator);
			};
		}		
		/*
		 * CREATING JOB PANEL AND BUTTONS
		 */
		public void ActivateJobPanel(GameObject jobPanel)
		{
			foreach (var panel in _jobPanels)
			{
				panel.SetActive(ReferenceEquals(panel, jobPanel));
			}
		}

		private void AddJobButton(GameObject responsible, IEnumerator enumerator)
		{
			var button = Instantiate(jobButtonPrefab, responsible.GetComponent<Responsible>().JobPanel.transform);
			ButtonUtil.AdjustPosition(button, 1);
			JobUtil.AddButton(responsible.GetComponent<Responsible>().ButtonList, button);
			ButtonUtil.SetOnClickAction(button, GetJobButtonAction(enumerator, button));
		}
		
		private UnityAction GetJobButtonAction(IEnumerator enumerator, GameObject button)
		{
			return delegate {
				ActionManager.Instance.Responsible.GetComponent<Responsible>().StopDoingJob(enumerator);
				ButtonUtil.Destroy(button);
			};
		}

		public void SetInfoPanel(Responsible responsible)
		{
			responsibleName.GetComponent<Text>().text = responsible.Name;
			SetNeeds(responsible);
		}

		private void SetNeeds(Responsible responsible)
		{
			foreach (Transform child in needParent.transform)
			{
				Destroy(child.gameObject);
			}

			var i = 0;
			foreach (var need in responsible.NeedList)
			{
				var newNeed = Instantiate(needPrefab, new Vector3(10f, 60-20f*i, 0f), Quaternion.identity, needParent.transform);
				newNeed.GetComponent<NeedBox>().SetNeed(need);
				i++;
			}
		}
	}
}
