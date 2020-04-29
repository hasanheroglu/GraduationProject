using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public Responsible Responsible { get; set; }
    public Dictionary<EquipableType, GameObject> Items { get; set; }
    public Weapon Weapon { get; set; }

    public GameObject weaponPosition;
    
    public Equipment(Responsible responsible)
    {
        Responsible = responsible;
        weaponPosition = Responsible.transform.Find("WeaponPosition").gameObject;
        Items = new Dictionary<EquipableType, GameObject>();
        
        /*
        var weaponGo = WeaponFactory.GetFist(Responsible.transform.position);
        weaponGo.transform.SetParent(Responsible.weaponPosition.transform);
        weaponGo.transform.position = Responsible.weaponPosition.transform.position;
        weaponGo.transform.rotation = Responsible.gameObject.transform.rotation;
        weaponGo.GetComponent<Rigidbody>().isKinematic = true;

        Items.Add(EquipableType.Weapon, weaponGo);
        Weapon = weaponGo.GetComponent<Weapon>();
        */
    }
    
    public void Equip(GameObject item)
    {
        var type = item.GetComponent<Equipable>().type;
        item.GetComponent<Rigidbody>().isKinematic = true;

        if (type == EquipableType.Weapon)
        {
            Debug.Log("Equipment is a weapon!");
            item.transform.SetParent(weaponPosition.transform);
            item.transform.position = weaponPosition.transform.position;
            item.transform.rotation = Responsible.gameObject.transform.rotation;
            Weapon = item.GetComponent<Weapon>();
            Weapon.Responsible = Responsible;
        }
        
        if (Items.Count == 0)
        {
            Items.Add(type, item);
        }
        else
        {
            if (Items.ContainsKey(type))
            {
                if (Items[type] != null)
                {
                    Unequip(Items[type]);
                }
                else
                {
                    Items[type] = item;
                }
            }
            else
            {
                Items.Add(type , item);
            }
        }
    }

    public void Unequip(GameObject item)
    {
        var type = item.GetComponent<Equipable>().type;

        if (type == EquipableType.Weapon)
        {
            Weapon = null;
        }
        
        if (Items.ContainsKey(type))
        {
            Items[type] = null;
            item.transform.parent = null;
            item.transform.position = new Vector3(0f, -100f, 0f);
        }
        else
        {
            Debug.Log("Equipable type does not exist!");
        }
    }

    public int GetArmorValue()
    {
        int armorValue = 0;
        
        foreach (var item in Items)
        {
            Armor armor = null;
            
            if(item.Value != null) armor = item.Value.GetComponent<Armor>();
            if(armor != null) armorValue += armor.Value;
        }

        return armorValue;
    }
}
