using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

public class CampFire : Interactable.Base.Interactable, ICookable
{
	public GameObject foodPrefab;
	
	private void Start()
	{
		InUse = 1;
		SetMethods();
	}

	[Activity(ActivityType.Cook)]
	[Skill(SkillType.Cooking, 500)]
	public IEnumerator Cook(Human human)
	{
		Debug.Log(human.name + "is cooking!");
		yield return new WaitForSeconds(5f);
		Debug.Log("Cooking finished!");
		human.FinishJob();
		var food = Instantiate(foodPrefab, this.transform.position, Quaternion.identity);
		var interactable = food.GetComponent<Interactable.Base.Interactable>();
		var coroutineInfo = new JobInfo(human, interactable,  interactable.GetComponent<IEdible>().GetType().GetMethod("Eat"), new object[] {human});
		UIManager.SetInteractionAction(human.gameObject, coroutineInfo, true);
	}
}
