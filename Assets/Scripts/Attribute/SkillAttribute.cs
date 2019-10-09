namespace Attribute
{
	public class SkillAttribute : System.Attribute
	{

		private SkillType _skillType;
		private float _earnedXp;

		public SkillAttribute(SkillType skillType, float earnedXp = 0)
		{
			_skillType = skillType;
			_earnedXp = earnedXp;
		}

		public SkillType SkillType
		{
			get { return _skillType; }
			set { _skillType = value; }
		}

		public float EarnedXp
		{
			get { return _earnedXp; }
			set { _earnedXp = value; }
		}
	}
}

