using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class Armor : Equipable
{
    public int Value { get; set; }
    
    // Start is called before the first frame update
    private void Awake()
    {
        SetMethods();
        base.Awake();
        InUse = 1;
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

    [Interactable(typeof(Responsible))]
    public override IEnumerator Equip(Responsible responsible)
    {
        return base.Equip(responsible);
    }

    [Interactable(typeof(Responsible))]
    public override IEnumerator Unequip(Responsible responsible)
    {
        return base.Unequip(responsible);
    }
}
