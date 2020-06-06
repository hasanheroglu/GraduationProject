using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Interactable.Base;
using Interactable.Creatures;
using Interface;
using UnityEngine;

namespace Crafting
{
	public class Recipe
	{
		
		public string Name { get; set; }
		public List<Ingredient> Ingredients { get; set; }
		public MethodInfo ProductMethod { get; set; }
		public float Duration { get; set; }
		public ICraftable Craftable { get; set; }

		public Recipe(string name, List<Ingredient> ingredients, MethodInfo productMethod, float duration, ICraftable craftable)
		{
			Name = name;
			Ingredients = ingredients;
			ProductMethod = productMethod;
			Duration = duration;
			Craftable = craftable;
		}

		public IEnumerator Craft(Responsible responsible)
		{
			yield return Util.WaitForSeconds(responsible.GetCurrentJob(), Duration);
			
			foreach (var ingredient in Ingredients)
			{
				if (responsible.Inventory.FindCount(ingredient.Name) >= ingredient.Amount)
				{
					responsible.Inventory.Remove(ingredient.Name, ingredient.Amount);
				}
			}
		}
	}
}
