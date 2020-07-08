using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;

public class Rock : Interactable.Base.Interactable, IMinable
{
    private static int _instanceCount;

    [SerializeField] private float mineDuration;
    
    private static GameObject _stone;
    
    // Start is called before the first frame update
    void Awake()
    {
        SetGroupName("rock");
        instanceNo = _instanceCount;
        _instanceCount++;
        InUse = 1;
        mineDuration = 3f;
        _stone = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Stone");
        SetMethods();
    }

    [Activity(ActivityType.Mine)]
    [Interactable(typeof(Human))]
    public IEnumerator Mine(Responsible responsible)
    {
        yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), mineDuration);
        GroundUtil.Clear(gameObject.transform.position);
        responsible.Inventory.Add(CreateStone());
        Destroy(gameObject, 0.5f);
        responsible.FinishJob();
    }

    private GameObject CreateStone()
    {
        var stone = Instantiate(_stone, Vector3.zero, Quaternion.identity);
        stone.GetComponent<Stone>().SetPicked(true);
        return stone;
    }
}
