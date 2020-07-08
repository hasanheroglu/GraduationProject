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
		recipes = new List<Recipe>
		{
			RecipeFactory.GetWoodenGauntlets(this), 
			RecipeFactory.GetWoodenHelmet(this), 
			RecipeFactory.GetWoodenFaulds(this), 
			RecipeFactory.GetTable(this),
			RecipeFactory.GetShotgun(this)
		};
	}

	[Activity(ActivityType.Craft)]
	[Interactable(typeof(Human))]
	[Skill(SkillType.Crafting, 500)]
	public IEnumerator Craft(Responsible responsible)
	{
		yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
		RecipeManager.Instance.OpenCraftingMenu(recipes, responsible);
		yield return new WaitUntil(() => recipeSet);
		yield return currentRecipe.Craft(responsible);

		foreach (var quest in responsible.quests)
		{
			quest.Progress(ActivityType.Craft, currentRecipe.Name);
		}

		var product = RecipeManager.Instance.CreateProduct(currentRecipe, gameObject);
		product.GetComponent<Pickable>().SetPicked(true);
		responsible.Inventory.Add(product);
		
		responsible.FinishJob();
		ResetRecipe();
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
