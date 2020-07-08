using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public interface IMinable
{
    IEnumerator Mine(Responsible responsible);
}
