using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ground : Interactable, IWalkable {

	public void Walk(Human human)
	{
		human.gameObject.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
	}
}
