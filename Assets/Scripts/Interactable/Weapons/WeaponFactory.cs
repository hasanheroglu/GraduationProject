using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class WeaponFactory
{
    private static readonly GameObject Fist = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Fist");
    private static readonly GameObject Weapon = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Weapon");
    
    public static GameObject GetFist(Vector3 position)
    {
        var weapon = Object.Instantiate(Fist, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.Melee, 10, 2f, 2f);
        weapon.GetComponent<Weapon>().SetGroupName("fist");
        return weapon;
    }
    
    public static GameObject GetPistol(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.SingleShot, 15, 30f, 1f);
        weapon.GetComponent<Weapon>().SetGroupName("pistol");
        return weapon;
    }

    public static GameObject GetShotgun(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.MultipleShot, 20, 10f, 2f);
        weapon.GetComponent<Weapon>().SetGroupName("shotgun");
        return weapon;
    }

    public static WeaponPattern GetWeaponPattern(WeaponType weaponType)
    {
        WeaponPattern weaponPattern = null;
        
        switch (weaponType)
        {
            case WeaponType.Melee:
                weaponPattern = new MeleePattern();
                break;
            case WeaponType.SingleShot:
                weaponPattern = new SingleShotPattern();
                break;
            case WeaponType.MultipleShot:
                weaponPattern = new MultipleShotPattern();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
        }

        return weaponPattern;
    }
    
}
