using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem{
	
	public GameObject Item { get; set; }
	public int Count { get; set; }

	public InventoryItem(GameObject item)
	{
		Item = item;
		Count = 1;
	}

	public void Add(int count)
	{
		Count += count;
	}

	public void Remove(int count)
	{
		Count -= count;
	}
}
