using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Interactable.Base;
using Interactable.Environment;
using Manager;
using UnityEngine;

public class PlantActivity : Activity
{
    private static Activity _instance;

    public PlantActivity(ActivityType type) : base(type)
    {
        
    }
    
    public static Activity GetInstance()
    {
        if(_instance == null) _instance = new PlantActivity(ActivityType.Plant);
        return _instance;
    }

    public override bool Do(Responsible responsible)
    {
        Interactable.Base.Interactable interactable = null;
        MethodInfo method = null;
      
        foreach (var item in responsible.Inventory.Items)
        {
            var methodInfo = item.GetComponent<Interactable.Base.Interactable>()
                .FindAllowedAction(responsible, Type);
         
            if (methodInfo != null)
            {
                interactable = item.GetComponent<Interactable.Base.Interactable>();
                method = methodInfo;
                break;
            }
        }

        Ground ground = null;
      
        var interactableObjects = Physics.OverlapSphere(responsible.gameObject.transform.position, 4f);
        if (interactableObjects.Length <= 0) return false;

        foreach (var interactableObject in interactableObjects)
        {
            var groundObject = interactableObject.GetComponent<Ground>();
            if(groundObject == null) continue;
            if(groundObject.occupied) continue;

            ground = groundObject;
        }

        if (method == null || interactable == null) return false;
      
        var coroutineInfo = new JobInfo(responsible, ground, ground.GetType().GetMethod("Walk"), new object[] {responsible});
        UIManager.SetInteractionAction(coroutineInfo);
        coroutineInfo = new JobInfo(responsible, interactable, method, new object[] {responsible});
        UIManager.SetInteractionAction(coroutineInfo);
        return true;
    }
}
