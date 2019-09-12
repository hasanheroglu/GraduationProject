using System.Collections;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Interactable.Environment
{
	public class Obstacle : Interactable.Base.Interactable, IDestroyable, ICarriable{
	
		public IEnumerator Destroy(Human human)
		{
			yield return human.GetComponent<Responsible>().StartCoroutine("Walk", gameObject.transform.position);
			Debug.Log("Destroying the obstacle in 10 seconds!");
			yield return new WaitForSeconds(10);
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
}
