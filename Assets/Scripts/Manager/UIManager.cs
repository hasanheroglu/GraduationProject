using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Manager
{
	public class UIManager : MonoBehaviour
	{

		private static UIManager instance;
		
		public GameObject canvas;
	
		[Header("Interaction")]
		public GameObject interactionButton;
		public GameObject interactionPanel;
		[Header("Job")] 
		public GameObject jobButton;
		public GameObject jobPanel;


		public static UIManager Instance { get { return instance; } }
	
		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
			} else {
				instance = this;
			}
		}
		
		/*
		 * CREATING INTERACTION MENU AND BUTTON
		 */
		
		public GameObject CreateInteractionPanel(Vector3 position)
		{
			return Instantiate(interactionPanel, position, Quaternion.identity, canvas.transform);
		}

		public void AddInteractionButton(GameObject panel, ButtonInfo buttonInfo)
		{
			IEnumerator enumerator = InteractionUtil.CreateCoroutine(buttonInfo);

			if (enumerator == null) return;

			var button = CreateInteractionButton(panel, buttonInfo);
			ButtonUtil.SetOnClickAction(button, GetInteractionAction(enumerator, buttonInfo.Target.GetComponent<Interactable>(), button));
		}

		private UnityAction GetInteractionAction(IEnumerator enumerator, Interactable target, GameObject button)
		{
			return delegate {
				InteractionUtil.CloseInteractionMenu();
				ActionManager.Instance.responsible.GetComponent<Responsible>().AddJob(enumerator);
				ActionManager.Instance.responsible.GetComponent<Responsible>().AddTarget(target);
				//var coroutine = StartCoroutine(enumerator);
				AddJobButton(enumerator);
				
			};
			
		}

		private GameObject CreateInteractionButton(GameObject panel, ButtonInfo buttonInfo)
		{
			var button = Instantiate(interactionButton, panel.transform);
			
			ButtonUtil.AdjustPosition(button, -1);
			ButtonUtil.SetText(button, buttonInfo.Method.Name);
			return button;
		}
		
		/*
		 * CREATING JOB PANEL AND BUTTONS
		 */

		private void AddJobButton(IEnumerator enumerator)
		{
			var button = Instantiate(jobButton, jobPanel.transform);
			ButtonUtil.AdjustPosition(button, 1);
			ActionManager.Instance.responsible.GetComponent<Responsible>().AddButton(button);
			ButtonUtil.SetOnClickAction(button, GetJobButtonAction(enumerator, button));
		}
		
		private UnityAction GetJobButtonAction(IEnumerator enumerator, GameObject button)
		{
			return delegate {
				ActionManager.Instance.responsible.GetComponent<Responsible>().StopDoingJob(enumerator);
				ButtonUtil.Destroy(button);
			};
			
		}

		
		
		
	}
}
