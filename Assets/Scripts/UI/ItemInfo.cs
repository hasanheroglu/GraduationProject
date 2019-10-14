using System.Collections;
using System.Collections.Generic;
using Crafting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

	public GameObject name;
	public RawImage image;

	private void Start()
	{
		
	}

	public void SetItemInfo(string itemName, RawImage itemImage)
	{
		name.GetComponent<Text>().text = itemName;
		//image = itemImage;
	}
}
