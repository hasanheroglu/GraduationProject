using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class FistPattern : WeaponPattern
{
    public FistPattern()
    {
        damage = 5;
        range = 1f;
        delay = 1f;
    }

    public override void Use(GameObject weapon, Vector3 direction)
    {
        var duration = 0.2f;
        weapon.GetComponent<Weapon>().StartCoroutine(MoveWeapon(weapon, direction, duration));

        Debug.Log("Weapon used fist pattern!");
    }

    private IEnumerator MoveWeapon(GameObject weapon, Vector3 direction, float overTime)
    {
        float startTime = Time.time;
        Interactable.Base.Interactable interactable = null;
        
        while(Time.time < startTime + overTime)
        {
            weapon.transform.position = Vector3.Lerp(weapon.GetComponent<Weapon>().Responsible.weaponPosition.transform.position, weapon.GetComponent<Weapon>().Responsible.weaponPosition.transform.position + direction, (Time.time - startTime)/overTime);
            
            var colliders = Physics.OverlapBox(weapon.transform.position, weapon.transform.localScale / 2);

            foreach (var collider in colliders)
            {
                interactable = Util.GetInteractableFromCollider(collider);
                if (interactable != null && interactable == weapon.GetComponent<Weapon>().target.GetComponent<Interactable.Base.Interactable>())
                {
                    break;
                }
            }
            
            yield return null;
        }

        if (interactable != null)
        {
            interactable.health -= damage;
            //interactable.gameObject.GetComponentInParent<Rigidbody>().AddForce(direction * 20);
        }
        
        weapon.transform.position = weapon.GetComponent<Weapon>().Responsible.weaponPosition.transform.position;
    }
}
