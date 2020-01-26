using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemInfo : MonoBehaviour
{
    [SerializeField] private GameObject itemInfo;
    [SerializeField] private GameObject actionButton;

    public void SetItemInfo(string itemName, int count)
    {
        itemInfo.GetComponent<Text>().text = itemName + " x" + count;
    }

    public void SetActionButton(UnityAction action)
    {
        ButtonUtil.SetOnClickAction(actionButton, action);
    }

}
