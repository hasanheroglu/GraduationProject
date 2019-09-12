using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skills{Lumberjack, Cooking, Hunting, Gardening}

public class Skill
{
	private Skills skillType;
	private int level;
	private float totalXp;
	private float neededXp;
	private float neededXpMultiplier;

	public Skill(Skills skillType, int level, float totalXp, float neededXp, float neededXpMultiplier)
	{
		this.skillType = skillType;
		this.level = level;
		this.totalXp = totalXp;
		this.neededXp = neededXp;
		this.neededXpMultiplier = neededXpMultiplier;
	}

	public Skills SkillType
	{
		get { return skillType; }
		set { skillType = value; }
	}

	public int Level
	{
		get { return level; }
		set { level = value; }
	}

	public float TotalXp
	{
		get { return totalXp; }
		set { totalXp = value; }
	}

	public float NeededXp
	{
		get { return neededXp; }
		set { neededXp = value; }
	}

	public float NeededXpMultiplier
	{
		get { return neededXpMultiplier; }
		set { neededXpMultiplier = value; }
	}
	
	
}
