using System.Collections;
using System.Collections.Generic;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : Interactable, IDestroyable, ICarriable{
	
	public IEnumerator Destroy(Human human)
	{
		yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
		GameObject.Destroy(this.gameObject);
		Debug.Log("Destroyed by " + human.Name);
		human.GetComponent<Responsible>().FinishJob();
	}

	public IEnumerator Carry(Human human)
	{
		yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
		Debug.Log("Carried by " + human.Name);
		human.GetComponent<Responsible>().FinishJob();
	}

	public IEnumerator Hold(Human human)
	{
		yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
		Debug.Log("Held by " + human.Name);
		human.GetComponent<Responsible>().FinishJob();
	}

	
}
