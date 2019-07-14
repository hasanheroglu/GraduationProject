using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : Interactable, IDestroyable, ICarriable{
	
	public IEnumerator Destroy()
	{
		Human human = ActionManager.Instance.responsible.GetComponent<Human>();
		yield return human.GetComponent<AIManager>().StartCoroutine("Walk", gameObject.transform.position);
		Destroy(this.gameObject);
		Debug.Log("Destroyed by " + human.Name);
	}

	public IEnumerator Carry()
	{
		Human human = ActionManager.Instance.responsible.GetComponent<Human>();
		yield return human.GetComponent<AIManager>().StartCoroutine("Walk", gameObject.transform.position);
		Debug.Log("Carried by " + human.Name);
	}

	
}
