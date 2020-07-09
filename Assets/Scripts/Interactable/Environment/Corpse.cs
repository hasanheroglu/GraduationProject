using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interface;
using UnityEngine;

public class Corpse : Interactable.Base.Interactable, IEdible
{
    private static int _instanceCount;

    [SerializeField] private float eatDuration = 5f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instanceNo = _instanceCount;
        _instanceCount++;
        InUse = 1;
        SetMethods();
    }

    [Interactable(typeof(Zombie))]
    [Activity(ActivityType.Eat)]
    public IEnumerator Eat(Responsible responsible)
    {
        yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), eatDuration);
        responsible.Heal(20);
        Destroy(gameObject, 0.5f);
        responsible.FinishJob();
    }
}
