using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Interactable, IDestroyable{
	
	public void Destroy(Human human)
	{
		Debug.Log("Destroyed by " + human.Name);
	}
}
