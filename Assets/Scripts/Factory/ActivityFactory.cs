using System.Collections.Generic;

namespace Factory
{
	public static class ActivityFactory {
		public static Activity GetChop()
		{
			var effects = new List<Effect> {new Effect(NeedType.Energy, -0.6f), new Effect(NeedType.Hygiene, -0.5f)};
			return new Activity(ActivityType.Chop, effects);
		}

		public static Activity GetSleep()
		{
			var effects = new List<Effect> {new Effect(NeedType.Energy, 0.6f)};
			return new Activity(ActivityType.Sleep, effects);
		}
	
	}
}
