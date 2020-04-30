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
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.Melee, 10, 1f, 0f);
        weapon.GetComponent<Weapon>().Name = "Fist";
        return weapon;
    }
    
    public static GameObject GetPistol(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.SingleShot, 15, 30f, 1f);
        weapon.GetComponent<Weapon>().Name = "Pistol";
        return weapon;
    }

    public static GameObject GetShotgun(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeapon(WeaponType.MultipleShot, 20, 10f, 2f);
        weapon.GetComponent<Weapon>().Name = "Shotgun";
        return weapon;
    }

    public static WeaponPattern GetWeaponPattern(WeaponType weaponType, int damage, float range, float delay)
    {
        WeaponPattern weaponPattern = null;
        
        switch (weaponType)
        {
            case WeaponType.Melee:
                weaponPattern = new MeleePattern(damage, range, delay);
                break;
            case WeaponType.SingleShot:
                weaponPattern = new SingleShotPattern(damage, range, delay);
                break;
            case WeaponType.MultipleShot:
                weaponPattern = new MultipleShotPattern(damage, range, delay);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
        }

        return weaponPattern;
    }
    
}
