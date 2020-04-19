using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using UnityEngine;

public class Seed : Interactable.Base.Interactable, IPlantable
{
    public GameObject product;
    
    // Start is called before the first frame update
    void Start()
    {
        InUse = 1;
        SetMethods();
    }
    
    [Activity(ActivityType.Plant)]
    [Interactable(typeof(Human))]
    [Skill(SkillType.Gardening, 500)]
    public IEnumerator Plant(Responsible responsible)
    {
        Debug.Log("Planting the " + Name);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Planted the " + Name);
        responsible.Inventory.Remove(Name, 1);
        var newProduct = Instantiate(product, responsible.gameObject.transform.position, Quaternion.identity);
        newProduct.GetComponent<Plant>().SetStatus(false);
        Destroy(gameObject);
    }
}
