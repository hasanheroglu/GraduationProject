using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

namespace Interactable.Manager
{
	public class EffectManager {

		public static void Apply(Responsible responsible, List<Effect> effects)
		{
			foreach (var effect in effects)
			{
				if (!responsible.Needs.ContainsKey(effect.NeedType)) continue;
				responsible.Needs[effect.NeedType].StepValue = effect.StepValue;
			}
		}
		
		public static void Remove(Responsible responsible, List<Effect> effects)
		{
			foreach (var effect in effects)
			{
				if (!responsible.Needs.ContainsKey(effect.NeedType)) break;
				responsible.Needs[effect.NeedType].Reset();
			}			
		}
	}
}
