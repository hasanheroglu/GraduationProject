using Interactable.Base;
using Manager;

namespace Interactable.Manager
{
	public class SkillManager {

		public static void AddSkill(Responsible responsible, Skill skill)
		{
			if (skill.SkillType == SkillType.None){ return; }
			if (!responsible.Skills.ContainsKey(skill.SkillType)){ responsible.Skills.Add(skill.SkillType, skill); }
			UIManager.Instance.SetSkills(responsible);
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
