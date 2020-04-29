using Interactable.Base;
using Manager;

namespace Interactable.Manager
{
	public class SkillManager {

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
	}
}
