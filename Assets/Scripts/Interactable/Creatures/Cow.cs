using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interface;
using UnityEngine;

public class Cow : Responsible
{
    private static int _instanceCount;
    
    public void Start()
    {
        SetGroupName("cow");
        instanceNo = _instanceCount;
        _instanceCount++;
        Behaviour = new Behaviour(this.GetComponent<Responsible>());
        Behaviour.Activities = new List<Activity> {ActivityFactory.GetActivity(ActivityType.Eat)};
        Behaviour.SetActivity();
        SetMethods();
        InUse = 999;
    }

    [Interactable(typeof(Responsible))]
    [Activity(ActivityType.Kill)]
    public override IEnumerator Attack(Responsible responsible)
    {
        return base.Attack(responsible);
    }
    
}
