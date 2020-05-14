using System.Collections;
using System.Collections.Generic;
using Interactable.Environment;
using Interface;
using UnityEngine;

public static class Util
{
    public static Interactable.Base.Interactable GetInteractableFromCollider(Collider collider)
    {
        if (collider.gameObject.GetComponent<Interactable.Base.Interactable>() == null)
        {
            if (collider.transform.parent == null) return null;

            return collider.transform.parent.gameObject.GetComponent<Interactable.Base.Interactable>();
        }

        return collider.gameObject.GetComponent<Interactable.Base.Interactable>();
    }

    public static GameObject GetDamagableFromCollider(Collider collider)
    {
        if (collider.gameObject.GetComponent<IDamagable>() != null)
        {
            return collider.gameObject;
        }
        
        return collider.gameObject.transform.parent.gameObject.GetComponent<IDamagable>() != null ? collider.gameObject.transform.parent.gameObject : null;
    }

    public static Ground GetGroundFromCollider(Collider collider)
    {
        return collider.gameObject.GetComponent<Ground>();
    }

    public static IEnumerator WaitForSeconds(Job job, float duration)
    {
        job.SetProgressDuration(duration);
        yield return new WaitForSeconds(duration);
    }
}
