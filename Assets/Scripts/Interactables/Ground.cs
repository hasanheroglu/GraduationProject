using System.Collections;
using System.Collections.Generic;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;

public class Ground : Interactable, IWalkable
{

	public IEnumerator Walk()
	{
		Human human = ActionManager.Instance.responsible.GetComponent<Human>();
		yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
		human.GetComponent<Responsible>().FinishJob();
		Debug.Log("Finished walking!");
	}
}