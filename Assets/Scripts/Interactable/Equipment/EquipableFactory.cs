using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public static class EquipableFactory
{
    private static readonly GameObject Armor = Resources.Load<GameObject>("Prefabs/Interactables/Items/Armor");

    public static GameObject GetWoodenHelmet(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenHelmet";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Head;
        return armorObject;
    }

    public static GameObject GetWoodenBreastPlate(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenBreastPlate";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Torso;
        return armorObject;
    }

    public static GameObject GetWoodenPauldrons(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenPauldrons";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Shoulder;
        return armorObject;
    }

    public static GameObject GetWoodenGauntlets(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenGauntlets";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Arm;
        return armorObject;
    }

    public static GameObject GetWoodenFaulds(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenFaulds";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Leg;
        return armorObject;
    }

    public static GameObject GetWoodenGreaves(Vector3 position)
    {
        var armorObject = Object.Instantiate(Armor, position, Quaternion.identity);
        var armor = armorObject.GetComponent<Armor>();
        armor.Name = "WoodenGreaves";
        armor.Value = 100;
        armor.durability = 100;
        armor.type = EquipableType.Foot;
        return armorObject;
    }
}
