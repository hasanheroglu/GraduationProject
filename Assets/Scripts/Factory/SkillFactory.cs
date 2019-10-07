namespace Factory
{
	public static class SkillFactory{

		public static Skill GetLumberjack()
		{
			return new Skill(SkillType.Lumberjack, 1, 0, 1000, 1.2f);
		}

		public static Skill GetGardening()
		{
			return new Skill(SkillType.Gardening, 1, 0, 1000, 1.2f);
		}

		public static Skill GetHunting()
		{
			return new Skill(SkillType.Hunting, 1, 0, 1000, 1.2f);
		}
	}
}
