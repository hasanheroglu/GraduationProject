using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem{
	
	public List<GameObject> Items { get; set; }
	public string Name { get; set; }
	public int Count { get; set; }

	public InventoryItem(GameObject item)
	{
		Items = new List<GameObject> {item};
		Name = item.GetComponent<Interactable.Base.Interactable>().Name;
		Count = 1;
	}

	public void Add(GameObject item)
	{
		Items.Add(item);
		Count += 1;
	}

	public void Remove(int count = 0)
	{
		for (int i = 0; i < count; i++)
		{
			Items.RemoveAt(0);
			Count -= 1;
		}
	}
}
