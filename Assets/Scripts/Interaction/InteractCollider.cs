using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{

	private AIManager _parentAi;

	private void Start()
	{
		_parentAi = GetComponentInParent<AIManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (ReferenceEquals(other.gameObject, _parentAi.target))
		{
			_parentAi.targetInRange = true;
			Debug.Log("Target found!");
		}
	}
}
