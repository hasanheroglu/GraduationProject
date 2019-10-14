using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Crafting
{
	public class Recipe
	{
		
		public string Name { get; set; }
		public List<Item> Ingredients { get; set; }
		public GameObject Product { get; set; }
		public float Duration { get; set; }
		public ICraftable Craftable { get; set; }

		public Recipe(string name, List<Item> ingredients, GameObject product, float duration, ICraftable craftable)
		{
			Name = name;
			Ingredients = ingredients;
			Product = product;
			Duration = duration;
			Craftable = craftable;
		}

		public IEnumerator Craft(Responsible responsible)
		{
			foreach (var ingredient in Ingredients)
			{
				foreach (var item in responsible.Inventory)
				{
					if (item.Item.GetComponent<Interactable.Base.Interactable>().Name == ingredient.Name)
					{
						item.Remove(1);
					}
				}
			}
			yield return new WaitForSeconds(Duration);
		}
	}
}
