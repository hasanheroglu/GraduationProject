using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class Stone : Pickable
{
    private static int _instanceCount;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instanceNo = _instanceCount;
        _instanceCount++;
        InUse = 1;
        SetMethods();
        base.Awake();
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
}
