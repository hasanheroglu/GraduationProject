using System;

namespace Attribute
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ActivityAttribute : System.Attribute
	{

		private ActivityType _activityType;

		public ActivityAttribute(ActivityType activityType)
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
