using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IPlantable
{
    IEnumerator Plant(Responsible responsible);
}
