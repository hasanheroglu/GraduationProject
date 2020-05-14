using UnityEngine;

namespace Factory
{
	public static class NeedFactory
	{
		private static float _hungerStepValue = -0.01f;
		private static float _hygieneStepValue = -0.00f;
		private static float _funStepValue = -0.00f;
		private static float _energyStepValue = -0.01f;
		private static float _bladderStepValue = -0.00f;
		private static float _socialStepValue = -0.00f;
		
		public static Need GetHunger()
		{
			return new Need(NeedType.Hunger, ActivityType.Eat, _hungerStepValue);
		}
		
		public static Need GetHygiene()
		{
			return new Need(NeedType.Hygiene, ActivityType.None, _hygieneStepValue);
		}
		
		public static Need GetFun()
		{
			return new Need(NeedType.Fun, ActivityType.None, _funStepValue);
		}
		
		public static Need GetEnergy()
		{
			return new Need(NeedType.Energy, ActivityType.Sleep, _energyStepValue);
		}
		
		public static Need GetBladder()
		{
			return new Need(NeedType.Bladder, ActivityType.None, _bladderStepValue);
		}
		
		public static Need GetSocial()
		{
			return new Need(NeedType.Social, ActivityType.None, _socialStepValue);
		}
	}
}
