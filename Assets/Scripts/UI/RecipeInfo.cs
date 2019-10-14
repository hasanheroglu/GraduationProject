using System.Collections;
using System.Collections.Generic;
using Crafting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfo : MonoBehaviour
{

	private Recipe _recipe;
	public GameObject itemInfoPrefab;
	
	public GameObject itemInfo;
	public GameObject ingredients;
	public GameObject selectButton;
	
	public void  SetRecipeInfo(Recipe recipe)
	{
		_recipe = recipe;
		itemInfo.GetComponent<ItemInfo>().SetItemInfo(_recipe.Name, null);
		SetIngredients(recipe.Ingredients);
		SetSelectButton();
	}

	private void SetIngredients(List<Item> ingredientList)
	{
		foreach (var ingredient in ingredientList)
		{
			var itemInfo = Instantiate(itemInfoPrefab, ingredients.transform);
			itemInfo.GetComponent<ItemInfo>().SetItemInfo(ingredient.Name, null);
		}
	}

	private void SetSelectButton()
	{
		selectButton.GetComponent<Button>().onClick.AddListener(() => {_recipe.Craftable.SetRecipe(_recipe); RecipeManager.Instance.CloseCraftingMenu();});
	}

}
