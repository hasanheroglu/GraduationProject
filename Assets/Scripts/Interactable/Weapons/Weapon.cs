using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interactable.Creatures;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
{
	Melee,
	SingleShot,
	MultipleShot
};

public class Weapon: Equipable
{
	public Responsible Responsible { get; set; }

	private GameObject _target;
	
	[Header("Weapon")]
	[SerializeField] private WeaponType weaponType;
	[SerializeField] private int damage;
	[SerializeField] private float range;
	[SerializeField] private float delay;
	
	private WeaponPattern _weaponPattern;

	private void Awake()
	{
		InUse = 1;
		SetMethods();
		base.Awake();
	}

	private void Update()
	{
		if (_weaponPattern == null)
		{
			SetWeaponPattern(WeaponFactory.GetWeaponPattern(weaponType, damage, range, delay));
		}
	}

	public void SetWeapon(WeaponType weaponType, int damage, float range, float delay)
	{
		this.weaponType = weaponType;
		this.damage = damage;
		this.range = range;
		this.delay = delay;
		SetWeaponPattern(WeaponFactory.GetWeaponPattern(weaponType, damage, range, delay));
	}
	
	public WeaponPattern GetWeaponPattern()
	{
		return _weaponPattern;
	}

	private void SetWeaponPattern(WeaponPattern weaponPattern)
	{
		_weaponPattern = weaponPattern;
	}

	public Coroutine Use(Interactable.Base.Interactable interactable, Coroutine coroutine)
	{
		_target = interactable.gameObject;
		
		if (CheckTargetInRange())
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}

			coroutine = null;
			Responsible.StopWalking();
			Responsible.TargetInRange = true;
			_weaponPattern.Use(this.gameObject, new Vector3(_target.gameObject.transform.position.x - gameObject.transform.position.x, 0, _target.gameObject.transform.position.z - gameObject.transform.position.z));
			Responsible.TargetInRange = false;
		}
		else
		{
			if (coroutine != null) return coroutine;
		
			var circlePos = Vector3.forward * _weaponPattern.range;
			circlePos = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * circlePos;
			var destination =
				_target.GetComponent<Interactable.Base.Interactable>().interactionPoint.transform.position + circlePos;
			destination.y = 0;
			coroutine = StartCoroutine(Responsible.Walk(destination));
		}

		return coroutine;
	}

	public IEnumerator Reload()
	{
		yield return new WaitForSeconds(_weaponPattern.delay);
	}

	private bool CheckTargetInRange()
	{
		var responsiblePos = Responsible.gameObject.transform.position;
		var targetPos = _target.gameObject.transform.position;
		var distance = Vector2.Distance(new Vector2(responsiblePos.x, responsiblePos.z), new Vector2(targetPos.x, targetPos.z));
		
		return  distance <= _weaponPattern.range;
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Equip(Responsible responsible)
	{
		return base.Equip(responsible);
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Unequip(Responsible responsible)
	{
		return base.Unequip(responsible);
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Pick(Responsible responsible)
	{
		return base.Pick(responsible);
	}

	[Interactable(typeof(Responsible))]
	public override IEnumerator Drop(Responsible responsible)
	{
		return base.Drop(responsible);
	}
}
