using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace Manager
{
	public class ActionManager : MonoBehaviour
	{
		private static ActionManager instance;

		private GraphicRaycaster _graphicRaycaster;
		private PointerEventData _pointerEventData;
		private EventSystem _eventSystem;
		private Camera main;

		public GameObject responsible;
		public GameObject target;
		public GameObject canvas;
	
		public static ActionManager Instance { get { return instance; } }
	
		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
			} else {
				instance = this;
			}
		}
	
		private void Start()
		{
			_graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
			_eventSystem = GetComponent<EventSystem>();
			main = Camera.main;
		}
	
		private void Update () 
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				_pointerEventData = new PointerEventData(_eventSystem);
				_pointerEventData.position = Input.mousePosition;

				List<RaycastResult> raycastResults = new List<RaycastResult>();
				_graphicRaycaster.Raycast(_pointerEventData, raycastResults);


				if (raycastResults.Count == 0)
				{
					InteractionUtil.CloseInteractionMenu();

					if (responsible == null)
					{
						SetResponsible();
					}
					else
					{
						SetTarget();
						Interact();
					}
				}
			}

			
			
		}

		private void SetResponsible()
		{
			if (!Input.GetMouseButtonDown(0)) return;
			if (main == null) return;

			Ray ray = main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (!Physics.Raycast(ray, out hit)) return;
			
			if(hit.transform.gameObject.CompareTag("Human"))
			{
				responsible = hit.transform.gameObject;
			}
		}

		private void SetTarget()
		{
			if (Input.GetMouseButtonDown(0))
			{ 
				if (main == null) return;

				Ray ray = main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.CompareTag("Human"))
					{
						responsible = hit.transform.gameObject;
						//responsible.GetComponent<Responsible>.ShowInfo();
					}
				}
			}
		}

		private void Interact()
		{
			if (!Input.GetMouseButtonDown(1)) return;
			if (main == null) return;
			
			Ray ray = main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (!Physics.Raycast(ray, out hit)) return;
			if (hit.transform == null) return;
			
			target = hit.transform.gameObject;
			InteractionUtil.ShowInteractions(target, new object[] {responsible.GetComponent<Responsible>()}, Input.mousePosition);
		}
	}
}
