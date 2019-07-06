using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : Interactable, IDestroyable{
	
	public void Destroy(Human human)
	{
		Destroy(this.gameObject);
		Debug.Log("Destroyed by " + human.Name);
	}
}
