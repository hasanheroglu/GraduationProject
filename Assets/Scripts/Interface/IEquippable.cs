using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IEquippable
{
    IEnumerator Equip(Responsible responsible);
}
