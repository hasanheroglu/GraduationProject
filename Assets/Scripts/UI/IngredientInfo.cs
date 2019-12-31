using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfo : MonoBehaviour
{
    public GameObject itemInfo;
    public GameObject amount;
    
    public void SetIngredientInfo(Ingredient ingredient)
    {
        itemInfo.GetComponent<ItemInfo>().SetItemInfo(ingredient);
        amount.GetComponent<Text>().text = ingredient.Amount.ToString();
    }
}
