using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient: Item {
	public int Amount { get; set; }

	public Ingredient(string name, RawImage image, int amount)
	{
		Name = name;
		Image = image;
		Amount = amount;
	}

	public void AddAmountInfo(int amount)
	{
		
	}
}
