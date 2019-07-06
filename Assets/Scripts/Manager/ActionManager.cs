using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionManager : MonoBehaviour
{

	private LayerMask layerMask;
	
	public GameObject responsible;
	public GameObject target;

	private void Start()
	{
		layerMask = 1 << 5;
		layerMask = ~layerMask;
	}

	void Update () {
		if(responsible == null){
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
						responsible = null;
						target = null;
					}
				}
			}
			if(Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.transform.gameObject.CompareTag("Floor"))
					{
						Debug.Log("move to the destination.");
						responsible.GetComponent<NavMeshAgent>().SetDestination(hit.point);
					}
					else if(hit.transform != null){
						target = hit.transform.gameObject;
						Debug.Log("list available target interactions.");
						InteractionUtil.ShowInteractions(target, new object[] {responsible.GetComponent<Human>()});
					}
				}
			}
		}
	}	
}
