using System.Collections;
using System.Collections.Generic;
using Crafting;
using UnityEngine;

public class RecipeFactory: MonoBehaviour
{
	public GameObject kebab;
	
	public static RecipeFactory Instance { get; private set; }
	
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
	
	public Recipe GetKebab()
	{
		return new Recipe("Kebab", new List<Item>(), kebab, 5f, null);
	}
}
