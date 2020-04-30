using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IPlaceable
{
    IEnumerator Place(Responsible responsible);
}
