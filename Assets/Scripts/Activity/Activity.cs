using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public enum ActivityType{None, Walk, Cook, Sleep, Workout, Relax, Social, Chop, Eat, Harvest, Shower, Kill, Plant, Wander, Mine, Craft, Place}
public abstract class Activity
{ 
        public ActivityType Type { get; set; }

        public Activity(ActivityType type)
        {
                Type = type;
        }

        public abstract bool Do(Responsible responsible);
}
