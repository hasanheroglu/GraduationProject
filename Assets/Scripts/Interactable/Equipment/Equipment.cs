using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

[System.Serializable]
public class Equipment: MonoBehaviour
{
    private Responsible _responsible;
    private GameObject _defaultMelee;

    public GameObject weaponPosition;
    
    public Dictionary<EquipableType, GameObject> Items { get; set; }
    public Weapon Weapon { get; set; }
    
    
    private void Start()
    {
        _responsible = GetComponent<Responsible>();
        Items = new Dictionary<EquipableType, GameObject>();
        SetDefaultMelee();
        Equip(_defaultMelee);
    }
    
    private void SetDefaultMelee()
    {
        _defaultMelee = WeaponFactory.GetFist(_responsible.gameObject.transform.position);
        _defaultMelee.GetComponent<Renderer>().enabled = false;
    }
    
    public void Equip(GameObject item)
    {
        var type = item.GetComponent<Equipable>().type;
        item.GetComponent<Rigidbody>().isKinematic = true;
        
        if (Items.Count == 0)
        {
            Items.Add(type, item);
        }
        else
        {
            if (Items.ContainsKey(type))
            {
                item.GetComponent<Equipable>().SetEquipped(true);
                if (Items[type] != null)
                {
                    Debug.Log("Found similar equipment. Unequiping " + Items[type].GetComponent<Equipable>().GetGroupName());
                    Unequip(Items[type]);
                }
                
                Items[type] = item;
            }
            else
            {
                Items.Add(type , item);
            }
        }
        
        if (type == EquipableType.Weapon)
        {
            SetWeapon(item);
        }
    }
    
    public void Unequip(GameObject item)
    {
        var type = item.GetComponent<Equipable>().type;

        if (_defaultMelee == item) return;
        
        if (type == EquipableType.Weapon)
        {
            item.GetComponent<Collider>().enabled = true;
            Weapon = _defaultMelee.GetComponent<Weapon>();
        }
        
        if (Items.ContainsKey(type))
        {
            Items[type] = null;
            item.transform.parent = null;
            item.transform.position = new Vector3(0f, -100f, 0f);
            item.GetComponent<Equipable>().SetEquipped(false);
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
    
    private void SetWeapon(GameObject item)
    {
        item.transform.SetParent(weaponPosition.transform);
        item.transform.position = weaponPosition.transform.position;
        item.transform.localRotation = new Quaternion(0, 1f, 0, 1f);
        item.GetComponent<Collider>().enabled = false;
        Weapon = item.GetComponent<Weapon>();
        Weapon.Responsible = _responsible;
    }
}
