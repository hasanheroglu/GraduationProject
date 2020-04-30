using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using Interactable.Environment;
using UnityEngine;

public class Seed : Pickable, IPlantable
{
    private GameObject _product;
    
    // Start is called before the first frame update
    private void Awake()
    {
        InUse = 1;
        SetMethods();
        base.Awake();
    }
    
    [Activity(ActivityType.Plant)]
    [Interactable(typeof(Human))]
    [Skill(SkillType.Gardening, 500)]
    public IEnumerator Plant(Responsible responsible)
    {
        var ground = GroundUtil.FindGround(responsible.gameObject.transform.position);

        if (ground != null)
        {
            if (!ground.Occupied)
            {
                responsible.Target = ground.gameObject;
                yield return StartCoroutine(responsible.Walk(ground.gameObject.transform.position));
            }
            else
            {
                Debug.Log("Ground is occupied!");
                responsible.FinishJob(true);
                yield return null;
            }
        }
        
        Debug.Log("Planting the " + Name);
        responsible.animator.SetTrigger("plant");
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), 1f);
        if(ground != null)
            ground.Occupied = true;
        responsible.animator.ResetTrigger("plant");
        Debug.Log("Planted the " + Name);
        responsible.Inventory.Remove(gameObject);
        var newProduct = Instantiate(_product,  new Vector3(ground.gameObject.transform.position.x, _product.transform.position.y, ground.gameObject.transform.position.z), _product.transform.rotation);
        newProduct.GetComponent<Plant>().SetHarvestable(false);
        Destroy(gameObject, 1f);
        responsible.FinishJob();
    }

    [Interactable(typeof(Responsible))]
    public override IEnumerator Pick(Responsible responsible)
    {
        return base.Pick(responsible);
    }
    
    [Interactable(typeof(Responsible))]
    public override IEnumerator Drop(Responsible responsible)
    {
        return base.Drop(responsible);
    }

    public void SetProduct(GameObject product)
    {
        _product = product;
    }
    
}
