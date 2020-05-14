using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Interactable.Base;
using Manager;
using UnityEngine;

public class ChopActivity : Activity
{
    private static Activity _instance;

    public ChopActivity(ActivityType type) : base(type)
    {
        
    }
    
    public static Activity GetInstance()
    {
        if(_instance == null) _instance = new ChopActivity(ActivityType.Chop);
        return _instance;
    }

    public override bool Do(Responsible responsible)
    {
        var interactableObjects = Physics.OverlapSphere(responsible.gameObject.transform.position, 10f);
        if (interactableObjects.Length <= 0) return false;

        foreach (var interactableObject in interactableObjects)
        {
            if(interactableObject.GetComponent<Interactable.Base.Interactable>() == null) continue;
         
            var method = interactableObject.GetComponent<Interactable.Base.Interactable>()
                .FindAllowedAction(responsible, Type);
         
            if(method == null) continue;

            var interactable = interactableObject.GetComponent<Interactable.Base.Interactable>();
         
            var coroutineInfo = new JobInfo(responsible, interactable, method, new object[] {responsible});
            UIManager.SetInteractionAction(coroutineInfo);
            return true;
        }

        return false;
    }
}
