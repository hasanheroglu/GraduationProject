using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEngine;

public class HarvestActivity : Activity
{
    private static Activity _instance;

    public HarvestActivity(ActivityType type) : base(type)
    {
        
    }

    public static Activity GetInstance()
    {
        if(_instance == null) _instance = new HarvestActivity(ActivityType.Harvest);
        return _instance;
    }

    public override bool Do(Responsible responsible)
    {
        var interactableObjects = Physics.OverlapSphere(responsible.gameObject.transform.position, 10f);
        if (interactableObjects.Length <= 0) return false;

        foreach (var interactableObject in interactableObjects)
        {
            var interactable = Util.GetInteractableFromCollider(interactableObject);
			
            if (!interactable) continue;
            if(ReferenceEquals(interactable, responsible)) continue;

            var method = interactable.FindAllowedAction(responsible, Type);
            if (method == null) continue;
			
            var coroutineInfo = new JobInfo(responsible, interactable, method, new object[] {responsible});
            UIManager.SetInteractionAction(coroutineInfo);
            return true;
        }

        return false;
    }
}
