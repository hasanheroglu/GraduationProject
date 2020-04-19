using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponFactory
{
    private static readonly GameObject Weapon = Resources.Load<GameObject>("Prefabs/Interactables/Environment/Weapon");

    public static GameObject GetFist(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeaponPattern(new FistPattern());
        return weapon;
    }
    
    public static GameObject GetPistol(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeaponPattern(new PistolPattern());
        return weapon;
    }

    public static GameObject GetShotgun(Vector3 position)
    {
        var weapon = Object.Instantiate(Weapon, position, Quaternion.identity);
        weapon.GetComponent<Weapon>().SetWeaponPattern(new ShotgunPattern());
        return weapon;
    }
    
}
