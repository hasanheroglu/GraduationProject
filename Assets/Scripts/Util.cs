using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public static class Util
{
    public static Interactable.Base.Interactable GetInteractableFromCollider(Collider collider)
    {
        return collider.gameObject.GetComponent<Interactable.Base.Interactable>() ?? collider.transform.parent.gameObject.GetComponent<Interactable.Base.Interactable>();
    }

    public static IDamagable GetDamagableFromCollider(Collider collider)
    {
        return collider.gameObject.GetComponent<IDamagable>() ?? collider.transform.parent.gameObject.GetComponent<IDamagable>();
    }
}
