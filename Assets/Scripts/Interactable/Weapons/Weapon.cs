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
	public bool CanUse { get; set; }

	private GameObject _target;
	private WeaponPattern _weaponPattern;

	[Header("Weapon")]
	[SerializeField] private WeaponType weaponType;
	[SerializeField] private float delay;
	
	[SerializeField] public int damage;
	[SerializeField] public float range;
	[SerializeField] public GameObject bulletPoint;
	
	private void Awake()
	{
		InUse = 1;
		SetMethods();
		CanUse = true;
		base.Awake();
	}

	private void Update()
	{
		if (_weaponPattern == null)
		{
			SetWeaponPattern(WeaponFactory.GetWeaponPattern(weaponType));
		}
	}

	public void SetWeapon(WeaponType weaponType, int damage, float range, float delay)
	{
		this.weaponType = weaponType;
		this.damage = damage;
		this.range = range;
		this.delay = delay;
		SetWeaponPattern(WeaponFactory.GetWeaponPattern(weaponType));
	}
	
	public WeaponPattern GetWeaponPattern()
	{
		return _weaponPattern;
	}

	private void SetWeaponPattern(WeaponPattern weaponPattern)
	{
		_weaponPattern = weaponPattern;
	}

	public IEnumerator Use()
	{
		if (CanUse)
		{
			_weaponPattern.Use(gameObject, GetDirection());
			yield return StartCoroutine(Wait());
		}
	}

	public IEnumerator Wait()
	{
		CanUse = false;
		yield return new WaitForSeconds(delay);
		CanUse = true;
	}

	private Vector3 GetDirection()
	{
		var respPos = gameObject.transform.position;
		var targetPos = _target.gameObject.transform.position;
		return new Vector3(targetPos.x - respPos.x, 0, targetPos.z - respPos.z);
	}

	public Vector3 GetInRangePosition()
	{
		var randPos = Random.insideUnitCircle * range;
		var destination = _target.GetComponent<Interactable.Base.Interactable>().interactionPoint.transform.position;
		destination += new Vector3(randPos.x, 0, randPos.y);
		destination.y = 0;
		return destination;
	}

	public void SetTarget(GameObject target)
	{
		_target = target;
	}

	public bool CheckTargetInRange()
	{
		var responsiblePos = Responsible.gameObject.transform.position;
		var targetPos = _target.gameObject.transform.position;
		var distance = Vector2.Distance(new Vector2(responsiblePos.x, responsiblePos.z), new Vector2(targetPos.x, targetPos.z));
		
		return  distance <= range;
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
