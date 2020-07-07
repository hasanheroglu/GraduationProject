using System.Reflection;
using Attribute;
using Interactable.Base;
using Manager;
using UnityEngine;

namespace Interactable.Manager
{
	public class SkillManager 
	{

		public static void AddSkill(Responsible responsible, Skill skill)
		{
			if (skill.Type == SkillType.None){ return; }
			if (!responsible.Skills.ContainsKey(skill.Type)){ responsible.Skills.Add(skill.Type, skill); }
		}

		public static void UpdateSkill(Responsible responsible, SkillType skillType, float xp)
		{
			if (responsible.Skills.ContainsKey(skillType))
			{
				responsible.Skills[skillType].LevelUp(xp);
			}
		}

		public static float GetSkillBonusForDuration(Responsible responsible, float duration)
		{
			SkillAttribute  skillAttribute = System.Attribute.GetCustomAttribute(responsible.GetCurrentJob().Method, typeof(SkillAttribute)) as SkillAttribute;
			
			if (skillAttribute == null) return duration;
			
			var reducedDuration = duration;
			if (responsible.Skills.ContainsKey(skillAttribute.SkillType))
			{
				reducedDuration *= (float)
					(responsible.Skills[skillAttribute.SkillType].MaxLevel -
					 responsible.Skills[skillAttribute.SkillType].Level + 1) /
					responsible.Skills[skillAttribute.SkillType].MaxLevel;
			}

			Debug.Log("Reduced duration: " + reducedDuration);
			
			return reducedDuration;
		}
	}
}
