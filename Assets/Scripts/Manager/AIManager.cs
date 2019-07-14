using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{

	private NavMeshAgent _agent;
	
	public bool targetInRange;
	public GameObject target;

	private void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
	}
	
	public IEnumerator Walk(Vector3 position)
	{
		_agent.SetDestination(position);
		yield return new WaitUntil((() => targetInRange));
		targetInRange = false;
	}


}
