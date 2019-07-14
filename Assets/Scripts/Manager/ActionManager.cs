using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
	private static ActionManager instance;

	private GraphicRaycaster _graphicRaycaster;
	private PointerEventData _pointerEventData;
	private EventSystem _eventSystem;

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
			}
		}

		if(responsible == null)
		{
			if (Input.GetMouseButtonDown(0))
			{ 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.CompareTag("Human"))
					{
						responsible = hit.transform.gameObject;
					}
				}
			}
		}
		else{
			if (Input.GetMouseButtonDown(0))
			{ 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.CompareTag("Human"))
					{
						responsible = hit.transform.gameObject;
					}
					else{
						//responsible = null;
						//target = null;
						//responsible.GetComponent<AIManager>().target = target;
					}
				}
			}
			if(Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform != null){
						target = hit.transform.gameObject;
						responsible.GetComponent<AIManager>().target = target;
 						InteractionUtil.ShowInteractions(target, new object[] {responsible.GetComponent<Human>()});
					}
				}
			}
		}
	}	
}
