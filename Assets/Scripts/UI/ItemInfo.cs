using System.Collections;
using System.Collections.Generic;
using Crafting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

	public GameObject name;
	public RawImage image;

	private void Start()
	{
		
	}

	public void SetItemInfo(string itemName, RawImage itemImage)
	{
		name.GetComponent<Text>().text = itemName;
		//image = itemImage;
	}
	
	public void SetItemInfo(Item item)
	{
		name.GetComponent<Text>().text = item.Name;
		//image = item.Image;
	}

	public void SetIngredientInfo(Ingredient ingredient)
	{
		name.GetComponent<Text>().text = ingredient.Amount.ToString() + " " + ingredient.Name;
		//image = item.Image;

	}

	public void AddAmountInfo(int amount)
	{
		name.GetComponent<Text>().text = amount + "/" + name.GetComponent<Text>().text;
	}
}
