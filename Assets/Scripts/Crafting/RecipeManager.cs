using System.Collections;
using System.Collections.Generic;
using Crafting;
using Interactable.Base;
using Interface;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
	private Job _job;

	public GameObject craftingMenu;
	public GameObject recipes;
	public GameObject recipeInfoPrefab;

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
	}
	
	public void OpenCraftingMenu(List<Recipe> recipeList, Responsible responsible)
	{
		craftingMenu.SetActive(true);
		SetRecipes(recipeList, responsible.Inventory.Items);
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

	private void SetRecipes(List<Recipe> recipeList, List<InventoryItem> items)
	{
		foreach (Transform child in recipes.transform)
		{
			Destroy(child.gameObject);
		}
		
		var i = 0;
		foreach (var recipe in recipeList)
		{
			var recipeInfo = Instantiate(recipeInfoPrefab, recipes.transform);
			recipeInfo.GetComponent<RecipeInfo>().SetRecipeInfo(recipe);
			recipeInfo.GetComponent<RecipeInfo>().selectButton.GetComponent<Button>().interactable = false;
			var recipeInfoRect = recipeInfo.GetComponent<RectTransform>().rect;
			var recipeInfoPosition = new Vector3(1, -1*i*recipeInfoRect.height, 0);
			recipeInfo.GetComponent<RectTransform>().anchoredPosition = recipeInfoPosition;

			foreach (var ingredient in recipe.Ingredients)
			{
				foreach (var item in items)
				{
					
					if (item.Name == ingredient.Name && item.Count >= ingredient.Amount)
					{
						recipeInfo.GetComponent<RecipeInfo>().selectButton.GetComponent<Button>().interactable = true;
					}
				}
			}

			i++;
		}
	}

	public GameObject CreateProduct(Recipe recipe, GameObject parent)
	{
		return Instantiate(recipe.Product, parent.transform.position, Quaternion.identity);
	}
}
