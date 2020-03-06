using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {Head, Shoulder, Arm, Torso, Leg, Foot}
public class EquipmentItem
{
    public int Armor { get; set; }
    public int Durability { get; set; }
    public EquipmentType EquipmentType { get; set; }
}
