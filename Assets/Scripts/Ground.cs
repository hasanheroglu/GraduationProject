using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ground : Interactable, IWalkable
{

	public IEnumerator Walk()
	{
		Human human = ActionManager.Instance.responsible.GetComponent<Human>();
		yield return human.GetComponent<AIManager>().StartCoroutine("Walk", gameObject.transform.position);
	}
}