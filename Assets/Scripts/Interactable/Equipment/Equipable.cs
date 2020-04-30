using System.Collections;
using Interactable.Base;
using UnityEngine;

public enum EquipableType {Head, Shoulder, Arm, Torso, Leg, Foot, Weapon}

public abstract class Equipable : Pickable
{
    protected bool equipped;

    [Header("Equipable")]
    [SerializeField] private float _equipDuration;
    [SerializeField] private float _unequipDuration;
    [SerializeField] public int durability; 
    [SerializeField] public EquipableType type;

    protected void Awake()
    {
        base.Awake();
        _equipDuration = .5f;
        _unequipDuration = .5f;
        SetEquipped(false);
    }
    
    public virtual IEnumerator Equip(Responsible responsible)
    {
        if (picked)
        {
            yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _equipDuration);
            responsible.Equipment.Equip(gameObject);
        }
        else
        {
            yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
            responsible.Inventory.Add(this.gameObject);
            yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _equipDuration);
            responsible.Equipment.Equip(gameObject);
        }
        
        SetEquipped(true);
        SetPicked(true);
        responsible.FinishJob();
    }

    public virtual IEnumerator Unequip(Responsible responsible)
    {
        if (equipped)
        {
            yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _unequipDuration);
            responsible.Equipment.Unequip(gameObject);
            SetEquipped(false);
        }

        yield return null;
        responsible.FinishJob();
    }

    public void SetEquipped(bool equippedStatus)
    {
        if (equippedStatus)
        {
            Methods.Remove(GetType().GetMethod("Equip"));
            Methods.Add(GetType().GetMethod("Unequip"));
        }
        else
        {
            Methods.Remove(GetType().GetMethod("Unequip"));
            Methods.Add(GetType().GetMethod("Equip"));
        }

        equipped = equippedStatus;
    }
    
    public new virtual IEnumerator Drop(Responsible responsible)
    {
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), dropDuration);
        if(equipped) responsible.Equipment.Unequip(gameObject);
        responsible.Inventory.Remove(gameObject);
        gameObject.transform.position = responsible.gameObject.transform.position;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        SetPicked(false);
        SetEquipped(false);
        responsible.FinishJob();    
    }
}
