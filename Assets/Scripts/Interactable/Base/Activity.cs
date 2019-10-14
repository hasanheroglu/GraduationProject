using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityType{None, Walk, Cook, Sleep, Workout, Relax, Social, Chop, Eat, Harvest}
public class Activity
{
        public ActivityType ActivityType { get; set; }
        public List<Effect> Effects { get; set; }

        public Activity(ActivityType activityType, List<Effect> effects)
        {
                ActivityType = activityType;
                Effects = effects;
        }
}
