namespace Factory
{
	public static class SkillFactory{


		public static Skill GetSkill(SkillType skillType)
		{
			switch (skillType)
			{
				case SkillType.Lumberjack:
					return GetLumberjack();
				case SkillType.Gardening:
					return GetGardening();
				case SkillType.Hunting:
					return GetHunting();
				case SkillType.Cooking:
					return GetCooking();
				case SkillType.Crafting:
					return GetCrafting();
				default:
					return GetNone();
			}
		}

		private static Skill GetNone()
		{
			return new Skill(SkillType.None, 0, 0, 0, 0);
		}
		
		private static Skill GetLumberjack()
		{
			return new Skill(SkillType.Lumberjack, 1, 0, 1000, 1.2f);
		}

		private static Skill GetGardening()
		{
			return new Skill(SkillType.Gardening, 1, 0, 1000, 1.2f);
		}

		private static Skill GetHunting()
		{
			return new Skill(SkillType.Hunting, 1, 0, 1000, 1.2f);
		}
		
		private static Skill GetCooking()
		{
			return new Skill(SkillType.Cooking, 1, 0, 1000, 1.2f);
		}

		private static Skill GetCrafting()
		{
			return new Skill(SkillType.Crafting, 1, 0, 1000, 1.2f);
		}
	}
}
