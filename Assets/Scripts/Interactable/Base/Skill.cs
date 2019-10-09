using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType{None, Lumberjack, Cooking, Hunting, Gardening}

public class Skill
{
	public Skill(SkillType skillType, int level, float totalXp, float neededXp, float neededXpMultiplier)
	{
		SkillType = skillType;
		Level = level;
		TotalXp = totalXp;
		NeededXp = neededXp;
		NeededXpMultiplier = neededXpMultiplier;
	}

	public SkillType SkillType { get; set; }

	public int Level { get; set; }

	public float TotalXp { get; set; }

	public float NeededXp { get; set; }

	public float NeededXpMultiplier { get; set; }

	public void LevelUp(float xp)
	{
		TotalXp += xp;
		
		if (TotalXp < NeededXp) return;
		
		TotalXp -= NeededXp;
		NeededXp *= NeededXpMultiplier;
		Level++;
	}	
}
