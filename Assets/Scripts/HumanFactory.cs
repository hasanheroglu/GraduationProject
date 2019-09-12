using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanFactory : MonoBehaviour
{
    public GameObject humanName;
    public GameObject responsiblePrefab;
    
    public void CreateHuman()
    {
        GameObject responsible = Instantiate(responsiblePrefab, new Vector3(0f, 5f, 0f), Quaternion.identity);
        responsible.GetComponent<Interactable.Base.Interactable>().Name = humanName.GetComponent<Text>().text;
    } 
}
