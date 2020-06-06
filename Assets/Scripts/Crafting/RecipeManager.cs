using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Crafting;
using Interactable.Base;
using Interface;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
	private Job _job;
	private static GameObject _recipeInfoPrefab;

	[SerializeField] private GameObject craftingMenu;
	[SerializeField] private GameObject recipes;

	public static RecipeManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
		}
		
		_recipeInfoPrefab = Resources.Load<GameObject>("Prefabs/UI/RecipeInfo");
	}
	
	public void OpenCraftingMenu(List<Recipe> recipeList, Responsible responsible)
	{
		craftingMenu.SetActive(true);
		SetRecipeMenu(recipeList, responsible.Inventory);
		_job = responsible.Jobs[0];
	}

	public void CloseCraftingMenu(bool immediate = false)
	{
		if (immediate)
		{
			_job.Stop(true);
		}
		craftingMenu.SetActive(false);	
	}

	private void SetRecipeMenu(List<Recipe> recipeList, Inventory inventory)
	{
		ClearRecipeMenu();
		SetRecipes(recipeList, inventory);
	}

	private void ClearRecipeMenu()
	{
		foreach (Transform child in recipes.transform)
		{
			Destroy(child.gameObject);
		}
	}
	
	private void SetRecipes(List<Recipe> recipeList, Inventory inventory)
	{
		var i = 0;
		foreach (var recipe in recipeList)
		{
			var recipeInfo = GetRecipeInfo(recipe, i);
			recipeInfo.GetComponent<RecipeInfo>().selectButton.GetComponent<Button>().interactable = CheckForIngredients(recipe, inventory);
			i++;
		}
	}

	private bool CheckForIngredients(Recipe recipe, Inventory inventory)
	{
		foreach (var ingredient in recipe.Ingredients)
		{
			if (inventory.FindCount(ingredient.Name) < ingredient.Amount)
			{
				return false;
			}
		}
		
		return true;
	}
	
	private GameObject GetRecipeInfo(Recipe recipe, int index)
	{
		var recipeInfo = Instantiate(_recipeInfoPrefab, recipes.transform);
		recipeInfo.GetComponent<RecipeInfo>().SetRecipeInfo(recipe);
		recipeInfo.GetComponent<RecipeInfo>().selectButton.GetComponent<Button>().interactable = false;
		var recipeInfoRect = recipeInfo.GetComponent<RectTransform>().rect;
		var recipeInfoPosition = new Vector3(1, -1*index*recipeInfoRect.height, 0);
		recipeInfo.GetComponent<RectTransform>().anchoredPosition = recipeInfoPosition;

		return recipeInfo;
	}

	public GameObject CreateProduct(Recipe recipe, GameObject parent)
	{
		return (GameObject) recipe.ProductMethod.Invoke(null, new object[] {parent.transform.position});
	}
}
