using UnityEngine;

namespace Factory
{
	public static class NeedFactory
	{
		private static float _hungerStepValue = -0.01f;
		private static float _hygieneStepValue = -0.1f;
		private static float _funStepValue = -0.1f;
		private static float _energyStepValue = -0.05f;
		private static float _bladderStepValue = -0.1f;
		private static float _socialStepValue = -0.1f;
		
		
		public static Need GetHunger()
		{
			return new Need(NeedType.Hunger, _hungerStepValue);
		}
		
		public static Need GetHygiene()
		{
			return new Need(NeedType.Hygiene, _hygieneStepValue);
		}
		
		public static Need GetFun()
		{
			return new Need(NeedType.Fun, _funStepValue);
		}
		
		public static Need GetEnergy()
		{
			return new Need(NeedType.Energy, _energyStepValue);
		}
		
		public static Need GetBladder()
		{
			return new Need(NeedType.Bladder, _bladderStepValue);
		}
		
		public static Need GetSocial()
		{
			return new Need(NeedType.Social, _socialStepValue);
		}
	}
}
