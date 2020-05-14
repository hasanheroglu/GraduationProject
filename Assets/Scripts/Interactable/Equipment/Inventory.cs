using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Interactable.Base;
using Manager;
using UnityEngine;

public class Inventory
{
    public Responsible Responsible { get; set; }
    public List<GameObject> Items { get; set; }
    
    public Inventory(Responsible responsible)
    {
        Responsible = responsible;
        Items = new List<GameObject>();
    }
    
    public void Add(GameObject item, bool unique = false)
    {
        item.transform.position = new Vector3(0f, -100f, 0f);
        Items.Add(item);
        UIManager.Instance.SetInventory(Responsible);
    }

    public GameObject Find(string itemName)
    {
        foreach (var item in Items)
        {
            if (item.GetComponent<Interactable.Base.Interactable>().GetGroupName() == itemName)
            {
                return item;
            }
        }

        return null;
    }

    public bool InInventory(GameObject item)
    {
        bool inInventory = false;
        foreach (var inventoryItem in Items)
        {
            if (item == inventoryItem)
            {
                inInventory = true;
                break;
            }
        }

        return inInventory;
    }
    
    public int FindCount(string itemName)
    {
        int count = 0;
        foreach (var item in Items)
        {
            if (item.GetComponent<Interactable.Base.Interactable>().GetGroupName() == itemName)
            {
                count++;
            }
        }

        return count;
    }

    public void Remove(string itemName)
    {
        var toBeRemoved = Find(itemName);
        Items.Remove(toBeRemoved);
        UIManager.Instance.SetInventory(Responsible);
    }
    
    public void Remove(GameObject item)
    {
        Items.Remove(item);
        UIManager.Instance.SetInventory(Responsible);
    }
}
