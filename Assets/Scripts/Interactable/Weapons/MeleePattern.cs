using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interface;
using UnityEngine;

public class MeleePattern : WeaponPattern
{
    private bool _canUse;

    public MeleePattern()
    {
        damage = 10;
        range = 1f;
        delay = 0f;
        _canUse = true;
    }
    
    public MeleePattern(int damage, float range, float delay)
    {
        this.damage = damage;
        this.range = range;
        this.delay = delay;
        this._canUse = true;
    }

    public override void Use(GameObject weapon, Vector3 direction)
    {
        var duration = 0.5f;

        if (_canUse)
        {
            weapon.GetComponent<Weapon>().StartCoroutine(MoveWeapon(weapon, direction, duration));
        }
    }

    private IEnumerator MoveWeapon(GameObject weapon, Vector3 direction, float overTime)
    {
        _canUse = false;
        float startTime = Time.time;
        var startPosition = weapon.GetComponent<Weapon>().Responsible.Equipment.weaponPosition.transform.position;
        var endPosition = startPosition + direction;
        GameObject damagable = null;
        HashSet<GameObject> damagables = new HashSet<GameObject>();
        weapon.GetComponent<Weapon>().Responsible.animator.SetBool("isPunching", true);
        
        while(Time.time < startTime + overTime)
        {
            weapon.transform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime)/overTime);
            
            var colliders = Physics.OverlapBox(weapon.transform.position, weapon.transform.localScale / 2);
            
            foreach (var collider in colliders)
            {
                damagable = Util.GetDamagableFromCollider(collider);
                
                if (damagable != null && !ReferenceEquals(damagable, weapon.GetComponent<Weapon>().Responsible.gameObject))
                {
                    damagables.Add(damagable);
                }
            }
            
            yield return null;
        }
        
        foreach (var damagableObject in damagables)
        {
            damagableObject.GetComponent<IDamagable>().Damage(damage);
            damagableObject.GetComponent<Rigidbody>().AddForce(direction.normalized * 200);
        }
        
        
        startTime = Time.time;
        while(Time.time < startTime + overTime)
        {
            weapon.transform.position = Vector3.Lerp(endPosition, startPosition, (Time.time - startTime)/overTime);
            yield return null;
        }
        
        weapon.transform.position = weapon.GetComponent<Weapon>().Responsible.Equipment.weaponPosition.transform.position;
        weapon.GetComponent<Weapon>().Responsible.animator.SetBool("isPunching", false);
        _canUse = true;
    }
}
