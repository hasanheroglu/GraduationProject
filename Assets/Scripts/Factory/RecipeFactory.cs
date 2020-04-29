using System.Collections;
using System.Collections.Generic;
using Crafting;
using Interface;
using UnityEngine;

public static class RecipeFactory
{
	public static Recipe GetKebab(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>()
			{new Ingredient("Onion", null, 2), new Ingredient("Flower", null, 2)};
		return new Recipe("Kebab", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenHelmet"), 5f, craftable);
	}

	public static Recipe GetTable(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 2)};
		return new Recipe("Table", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenHelmet"), 12f, craftable);
	}

	public static Recipe GetWoodenHelmet(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenHelmet"), 10f, craftable);
	}
	
	public static Recipe GetWoodenBreastPlate(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenBreastPlate"), 10f, craftable);
	}
	public static Recipe GetWoodenPauldrons(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenPauldrons"), 10f, craftable);
	}
	public static Recipe GetWoodenGreaves(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenGreaves"), 10f, craftable);
	}
	public static Recipe GetWoodenGauntlets(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenGauntlets"), 10f, craftable);
	}
	
	public static Recipe GetWoodenFaulds(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("Wood", null, 4)};
		return new Recipe("WoodenHelmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenFaulds"), 10f, craftable);
	}
}
