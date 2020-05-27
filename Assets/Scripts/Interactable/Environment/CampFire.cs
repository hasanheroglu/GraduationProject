using System.Collections;
using System.Collections.Generic;
using Attribute;
using Crafting;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

public class CampFire : Interactable.Base.Interactable, ICraftable
{
	private static int _instanceCount;
	
	public Recipe currentRecipe;
	public bool recipeSet;
	public List<Recipe> recipes;
	
	private void Awake()
	{
		SetGroupName("campfire");
		instanceNo = _instanceCount;
		_instanceCount++;
		InUse = 1;
		recipeSet = false;
		SetMethods();
		recipes = new List<Recipe> {RecipeFactory.GetWoodenGauntlets(this), RecipeFactory.GetWoodenHelmet(this), RecipeFactory.GetWoodenFaulds(this), RecipeFactory.GetTable(this)};
	}

	[Activity(ActivityType.Cook)]
	[Interactable(typeof(Human))]
	[Skill(SkillType.Cooking, 500)]
	public IEnumerator Craft(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		RecipeManager.Instance.OpenCraftingMenu(recipes, responsible);
		yield return new WaitUntil(() => recipeSet);
		yield return currentRecipe.Craft(responsible);
		responsible.FinishJob();
		var food = RecipeManager.Instance.CreateProduct(currentRecipe, gameObject);
		Eat(responsible, food);
		ResetRecipe();
	}

	private void Eat(Responsible responsible, GameObject food)
	{
		var interactable = food.GetComponent<Interactable.Base.Interactable>();
		var coroutineInfo = new JobInfo(responsible, interactable,  interactable.GetComponent<IEdible>().GetType().GetMethod("Eat"), new object[] {responsible});
		UIManager.SetInteractionAction(coroutineInfo, true);
	}

	public void SetRecipe(Recipe recipe)
	{
		currentRecipe = recipe;
		recipeSet = true;
	}

	public void ResetRecipe()
	{
		currentRecipe = null;
		recipeSet = false;
	}
}
