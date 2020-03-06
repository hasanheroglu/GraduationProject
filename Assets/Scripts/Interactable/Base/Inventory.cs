using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEngine;

public class Inventory
{
    public Responsible Responsible { get; set; }
    public List<InventoryItem> Items { get; set; }
    
    public Inventory(Responsible responsible)
    {
        Responsible = responsible;
        Items = new List<InventoryItem>();
    }
    
    public void Add(GameObject item)
    {
        foreach (var inventoryItem in Items)
        {
            if (inventoryItem.Name == item.GetComponent<Interactable.Base.Interactable>().Name)
            {
                inventoryItem.Add(item);
                UIManager.Instance.SetInventory(Responsible);
                return;
            }
        }
			
        Items.Add(new InventoryItem(item));
        UIManager.Instance.SetInventory(Responsible);
    }

    public void Remove(string itemName, int count)
    {
        foreach (var inventoryItem in Items)
        {
            if (inventoryItem.Name == itemName)
            {
                if (inventoryItem.Count == count)
                {
                    inventoryItem.Remove(count);
                    Items.Remove(inventoryItem);
                }
                else
                {
                    inventoryItem.Remove(count);
                }
					
                break;
            }
        }
			
        UIManager.Instance.SetInventory(Responsible);
    }
}
