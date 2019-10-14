using System.Collections;
using System.Collections.Generic;
using Attribute;
using Crafting;
using Interactable.Creatures;
using Interface;
using Manager;
using UnityEngine;

public class CampFire : Interactable.Base.Interactable, ICraftable
{
	public Recipe currentRecipe;
	public bool recipeSet;
	public List<Recipe> recipes;
	public GameObject kebabPrefab;
	
	private void Awake()
	{
		InUse = 1;
		recipeSet = false;
		SetMethods();
		recipes = new List<Recipe>();
		var ingredients = new List<Item>();
		var item = new Item("Onion", null);
		ingredients.Add(item);
		recipes.Add(new Recipe("Kebab", ingredients, kebabPrefab, 5.0f, this));
	}

	private void Update()
	{
		Debug.Log("I'm a camp fire and my InUse is " + InUse);
	}

	[Activity(ActivityType.Cook)]
	[Interactable(typeof(Human))]
	[Skill(SkillType.Cooking, 500)]
	public IEnumerator Craft(Human human)
	{
		Debug.Log(human.name + "is cooking!");
		RecipeManager.Instance.OpenCraftingMenu(recipes, human);
		yield return new WaitUntil(() => recipeSet);
		yield return currentRecipe.Craft(human);
		Debug.Log(currentRecipe.Name + "Cooking finished!");
		human.FinishJob();
		var food = RecipeManager.Instance.CreateProduct(currentRecipe, gameObject);
		Eat(human, food);
		ResetRecipe();
	}

	private void Eat(Human human, GameObject food)
	{
		var interactable = food.GetComponent<Interactable.Base.Interactable>();
		var coroutineInfo = new JobInfo(human, interactable,  interactable.GetComponent<IEdible>().GetType().GetMethod("Eat"), new object[] {human});
		UIManager.SetInteractionAction(human.gameObject, coroutineInfo, true);
	}

	public void SetRecipe(Recipe recipe)
	{
		currentRecipe = recipe;
		recipeSet = true;
	}

	private void ResetRecipe()
	{
		currentRecipe = null;
		recipeSet = false;
	}
}
