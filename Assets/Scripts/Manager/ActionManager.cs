using System.Collections.Generic;
using Interactable.Base;
using Interaction;
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
		private Camera _main;
		public GameObject _responsible;
		public GameObject _target;
		
		public GameObject canvas;

		public GameObject Responsible
		{
			get { return _responsible; }
			set { _responsible = value; }
		}

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
			_main = Camera.main;
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
					UIManager.Instance.CloseInteractionPanel();
					SetResponsible();

					if (_responsible)
					{
						Interact();
					}
				}
			}
		}

		private void SetResponsible()
		{
			if (!Input.GetMouseButtonDown(0)) return;
			if (_main == null) return;

			Ray ray = _main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (!Physics.Raycast(ray, out hit)) return;
			if (!hit.transform.gameObject.CompareTag("Human")) return;
			
			_responsible = hit.transform.gameObject;
			UIManager.Instance.ActivateJobPanel(_responsible.GetComponent<Responsible>().JobPanel);
			UIManager.Instance.SetInfoPanel(_responsible.GetComponent<Responsible>());
		}

		private void SetTarget()
		{
			if (Input.GetMouseButtonDown(0))
			{ 
				if (_main == null) return;

				Ray ray = _main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.CompareTag("Human"))
					{
						_responsible = hit.transform.gameObject;
						//responsible.GetComponent<Responsible>.ShowInfo();
					}
				}
			}
		}

		private void Interact()
		{
			if (!Input.GetMouseButtonDown(1)) return;
			if (_main == null) return;
			
			Ray ray = _main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (!Physics.Raycast(ray, out hit)) return;
			if (hit.transform == null) return;
			
			_target = hit.transform.gameObject;
			InteractionUtil.ShowInteractions(_responsible, _target, new object[] {_responsible.GetComponent<Responsible>()}, Input.mousePosition);
		}
	}
}
