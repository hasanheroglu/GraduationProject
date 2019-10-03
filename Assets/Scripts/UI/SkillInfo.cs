using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{

	private Skill _skill;
	private Text _levelText;
	private float _backgroundWidth;
	private RectTransform _progressRect;
	

	public GameObject name;
	public GameObject level;
	public GameObject progress;
	public GameObject progressBackground;

	private void Awake()
	{
		_levelText = level.GetComponent<Text>();
		_backgroundWidth = progressBackground.GetComponent<RectTransform>().rect.width;
		_progressRect = progress.GetComponent<RectTransform>();
	}

	private void Update()
	{		
		SetLevel();
		SetProgress();
	}

	public void SetSkill(Skill skill)
	{
		_skill = skill;
		SetSkillInfo();
	}

	private void SetSkillInfo()
	{
		SetName();
		SetLevel();
		SetProgress();
	}

	private void SetName()
	{
		this.name.GetComponent<Text>().text = _skill.SkillType.ToString();
	}

	private void SetLevel()
	{
		 _levelText.text = _skill.Level.ToString();
	}

	private void SetProgress()
	{
		_progressRect.sizeDelta = new Vector2((_skill.TotalXp / _skill.NeededXp) * _backgroundWidth, _progressRect.rect.height);
	}
}
