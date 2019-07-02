using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionManager : MonoBehaviour {

	public GameObject responsible;
	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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

					}
				}
			}
		}
	}
}
