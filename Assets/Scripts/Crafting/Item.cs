using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public string Name { get; set; }
    public RawImage Image { get; set; }

    private GameObject itemPrefab;

    protected Item()
    {
        Name = "";
        Image = null;
    }
    
    public Item(string name, RawImage image)
    {
        Name = name;
        Image = image;
    }
}
