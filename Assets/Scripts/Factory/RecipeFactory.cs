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
		return new Recipe("kebab", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenHelmet"), 5f, craftable);
	}

	public static Recipe GetTable(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 2)};
		return new Recipe("table", ingredients, typeof(ItemFactory).GetMethod("GetTable"), 12f, craftable);
	}
	
	public static Recipe GetPistol(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 1), new Ingredient("stone", null, 1)};
		return new Recipe("pistol", ingredients, typeof(WeaponFactory).GetMethod("GetPistol"), 6f, craftable);
	}
	
	public static Recipe GetShotgun(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 2), new Ingredient("stone", null, 1)};
		return new Recipe("shotgun", ingredients, typeof(WeaponFactory).GetMethod("GetShotgun"), 12f, craftable);
	}

	public static Recipe GetWoodenHelmet(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_helmet", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenHelmet"), 10f, craftable);
	}
	
	public static Recipe GetWoodenBreastPlate(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_breastplate", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenBreastPlate"), 10f, craftable);
	}
	public static Recipe GetWoodenPauldrons(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_pauldrons", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenPauldrons"), 10f, craftable);
	}
	public static Recipe GetWoodenGreaves(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_greaves", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenGreaves"), 10f, craftable);
	}
	public static Recipe GetWoodenGauntlets(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_gauntlets", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenGauntlets"), 10f, craftable);
	}
	
	public static Recipe GetWoodenFaulds(ICraftable craftable)
	{
		List<Ingredient> ingredients = new List<Ingredient>() {new Ingredient("wood", null, 4)};
		return new Recipe("wooden_faulds", ingredients, typeof(EquipableFactory).GetMethod("GetWoodenFaulds"), 10f, craftable);
	}
}
