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
   
   public static Activity GetActivity(ActivityType activityType)
   {
      switch (activityType)
      {
         case ActivityType.None:
            break;
         case ActivityType.Walk:
            break;
         case ActivityType.Cook:
            break;
         case ActivityType.Sleep:
            return GetSleep();
         case ActivityType.Workout:
            break;
         case ActivityType.Relax:
            break;
         case ActivityType.Social:
            break;
         case ActivityType.Chop:
            return GetChop();
         case ActivityType.Eat:
            return GetEat();
         case ActivityType.Harvest:
            return GetHarvest();
         case ActivityType.Shower:
            break;
         case ActivityType.Kill:
            return GetKill();
         case ActivityType.Plant:
            return GetPlant();
         default:
            throw new ArgumentOutOfRangeException();
      }

      return null;
   }
   

   public static Activity GetPlant()
   {
      return PlantActivity.GetInstance();
   }

   public static Activity GetKill()
   {
      return KillActivity.GetInstance();
   }

   public static Activity GetEat()
   {
      return EatActivity.GetInstance();
   }

   public static Activity GetChop()
   {
      return ChopActivity.GetInstance();
   }

   public static Activity GetHarvest()
   {
      return HarvestActivity.GetInstance();
   }

   public static Activity GetSleep()
   {
      return SleepActivity.GetInstance();
   }
}
