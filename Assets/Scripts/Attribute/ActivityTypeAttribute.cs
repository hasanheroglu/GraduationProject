using System;

namespace Attribute
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ActivityTypeAttribute : System.Attribute
	{

		private ActivityType _activityType;

		public ActivityTypeAttribute(ActivityType activityType)
		{
			_activityType = activityType;
		}

		public ActivityType ActivityType
		{
			get { return _activityType; }
			set { _activityType = value; }
		}

	}
}
