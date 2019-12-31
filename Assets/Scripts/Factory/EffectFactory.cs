using System;
using System.Collections.Generic;

namespace Factory
{
	public class EffectFactory
	{		
		public static void GetEffects(ActivityType activityType, List<Effect> effects)
		{
			switch (activityType)
			{
				case ActivityType.Sleep:
					effects.Add(GetSleep());
					break;
				case ActivityType.Workout:
					break;
				case ActivityType.Relax:
					break;
				case ActivityType.Social:
					break;
				case ActivityType.Chop:
					effects.Add(GetChop());
					effects.Add(GetHunger());
					break;
				case ActivityType.Eat:
					effects.Add(GetEat());
					break;
				case ActivityType.Harvest:
					break;
				case ActivityType.None:
					break;
				case ActivityType.Cook:
					break;
				case ActivityType.Walk:
					break;
				case ActivityType.Shower:
					effects.Add(GetShower());
					break;
				case ActivityType.Kill:
					break;
				default:
					throw new ArgumentOutOfRangeException("activityType", activityType, null);
			}
		}

		public static Effect GetSleep()
		{
			return new Effect(NeedType.Energy, 0.1f);
		}

		public static Effect GetEat()
		{
			return new Effect(NeedType.Hunger, 0.2f);
		}

		public static Effect GetChop()
		{
			return new Effect(NeedType.Energy, -0.2f);
		}

		public static Effect GetHunger()
		{
			return new Effect(NeedType.Hunger, -0.1f);
		}

		public static Effect GetShower()
		{
			return new Effect(NeedType.Hygiene, 0.1f);
		}

	}
	
}
