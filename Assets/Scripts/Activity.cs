using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityType{Sleep, Workout, Relax, Social, Chop}
public class Activity
{
        private ActivityType _activityType;
        private List<Effect> _effects;

        public ActivityType ActivityType
        {
                get { return _activityType; }
                set { _activityType = value; }
        }

        public List<Effect> Effects
        {
                get { return _effects; }
                set { _effects = value; }
        }
        
        public Activity(ActivityType activityType, List<Effect> effects)
        {
                _activityType = activityType;
                _effects = effects;
        }
}
