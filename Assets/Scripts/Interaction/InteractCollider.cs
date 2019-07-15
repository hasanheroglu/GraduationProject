using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{

	private Responsible _responsible;

	private void Start()
	{
		_responsible = GetComponentInParent<Responsible>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!ReferenceEquals(other.gameObject, _responsible.target)) return;
		
		_responsible.targetInRange = true;
		Debug.Log("Target found!");
	}
	
	private void OnTriggerStay(Collider other)
	{
		if (!ReferenceEquals(other.gameObject, _responsible.target)) return;
		
		_responsible.targetInRange = true;
		Debug.Log("Target found!");
	}
}
