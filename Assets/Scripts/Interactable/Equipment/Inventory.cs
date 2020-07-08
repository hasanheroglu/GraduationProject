using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Interactable.Base;
using Manager;
using UnityEngine;


public class Inventory: MonoBehaviour
{
    private Responsible _responsible;
    
    public List<GameObject> Items { get; set; }

    private void Start()
    {
        _responsible = GetComponent<Responsible>();
        Items = new List<GameObject>();
    }
    
    public void Add(GameObject item, bool unique = false)
    {
        item.transform.position = new Vector3(0f, -100f, 0f);
        Items.Add(item);
        UIManager.Instance.SetInventory(_responsible);
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

    public void Remove(string itemName, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var toBeRemoved = Find(itemName);
            Items.Remove(toBeRemoved);
        }
        
        UIManager.Instance.SetInventory(_responsible);
    }
    
    public void Remove(GameObject item)
    {
        Items.Remove(item);
        UIManager.Instance.SetInventory(_responsible);
    }
}
