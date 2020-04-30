using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Interactable.Base;
using Interactable.Environment;
using Interactable.Manager;
using Manager;
using UnityEngine;

public static class ActivityFactory
{
   public static void GetActivity(Behaviour behaviour)
   {
      switch (behaviour.Activity)
      {
         case ActivityType.None:
            break;
         case ActivityType.Walk:
            break;
         case ActivityType.Cook:
            break;
         case ActivityType.Sleep:
            break;
         case ActivityType.Workout:
            break;
         case ActivityType.Relax:
            break;
         case ActivityType.Social:
            break;
         case ActivityType.Chop:
            GetChopActivity(behaviour);
            break;
         case ActivityType.Eat:
            break;
         case ActivityType.Harvest:
            break;
         case ActivityType.Shower:
            break;
         case ActivityType.Kill:
            break;
         case ActivityType.Plant:
            GetPlantActivity(behaviour);
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }

   private static void GetChopActivity(Behaviour behaviour)
   {
      Interactable.Base.Interactable interactable = null;
      MethodInfo method = null;
      
      var interactableObjects = Physics.OverlapSphere(behaviour.Responsible.gameObject.transform.position, 10f);
      if (interactableObjects.Length <= 0) return;

      foreach (var interactableObject in interactableObjects)
      {
         if(interactableObject.GetComponent<Interactable.Base.Interactable>() == null) continue;
         
         method = interactableObject.GetComponent<Interactable.Base.Interactable>()
            .FindAllowedAction(behaviour.Responsible, behaviour.Activity);
         
         if(method == null) continue;

         interactable = interactableObject.GetComponent<Interactable.Base.Interactable>();
         
         var coroutineInfo = new JobInfo(behaviour.Responsible, interactable, method, new object[] {behaviour.Responsible});
         UIManager.SetInteractionAction(coroutineInfo);
         return;
      }
   }

   private static void GetPlantActivity(Behaviour behaviour)
   {
      Interactable.Base.Interactable interactable = null;
      MethodInfo method = null;
      
      foreach (var item in behaviour.Responsible.Inventory.Items)
      {
         var methodInfo = item.GetComponent<Interactable.Base.Interactable>()
            .FindAllowedAction(behaviour.Responsible, behaviour.Activity);
         
         if (methodInfo != null)
         {
            interactable = item.GetComponent<Interactable.Base.Interactable>();
            method = methodInfo;
            break;
         }
      }

      Ground ground = null;
      
      var interactableObjects = Physics.OverlapSphere(behaviour.Responsible.gameObject.transform.position, 50f);
      if (interactableObjects.Length <= 0) return;

      foreach (var interactableObject in interactableObjects)
      {
         var groundObject = interactableObject.GetComponent<Ground>();
         if(groundObject == null) continue;
         if(groundObject.Occupied) continue;

         ground = groundObject;
      }

      if (method == null || interactable == null) return;
      
      var coroutineInfo = new JobInfo(behaviour.Responsible, ground, ground.GetType().GetMethod("Walk"), new object[] {behaviour.Responsible});
      UIManager.SetInteractionAction(coroutineInfo);
      coroutineInfo = new JobInfo(behaviour.Responsible, interactable, method, new object[] {behaviour.Responsible});
      UIManager.SetInteractionAction(coroutineInfo);
   }
}
