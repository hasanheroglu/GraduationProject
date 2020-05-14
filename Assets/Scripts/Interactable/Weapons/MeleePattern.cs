using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interface;
using Manager;
using UnityEngine;

public class MeleePattern : WeaponPattern
{
    public MeleePattern()
    {

    }

    public override void Use(GameObject weapon, Vector3 direction)
    {
        if (!weapon.GetComponent<Weapon>().CanUse) return;
        
        var duration = 0.5f;
        weapon.GetComponent<Weapon>().StartCoroutine(MoveWeapon(weapon, direction, duration));
    }

    private IEnumerator MoveWeapon(GameObject weapon, Vector3 direction, float overTime)
    {
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
            if (damagableObject.GetComponent<IDamagable>().Damage(weapon.GetComponent<Weapon>().damage))
            {
                var killQuests = QuestManager.FindWithActivityType(weapon.GetComponent<Weapon>().Responsible, ActivityType.Kill);
                foreach (var quest in killQuests)
                {
                    quest.Progress(ActivityType.Kill, damagableObject.GetComponent<Interactable.Base.Interactable>().GetGroupName());
                }
                UIManager.Instance.SetQuests(weapon.GetComponent<Weapon>().Responsible);
            }
            damagableObject.GetComponent<Rigidbody>().AddForce(direction.normalized * 50);
        }
        
        
        startTime = Time.time;
        while(Time.time < startTime + overTime)
        {
            weapon.transform.position = Vector3.Lerp(endPosition, startPosition, (Time.time - startTime)/overTime);
            yield return null;
        }
        
        weapon.transform.position = weapon.GetComponent<Weapon>().Responsible.Equipment.weaponPosition.transform.position;
        weapon.GetComponent<Weapon>().Responsible.animator.SetBool("isPunching", false);
    }
}
