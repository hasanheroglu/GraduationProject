using System.Collections;
using System.Reflection;
using Interactable.Base;
using UnityEngine;

public abstract class Pickable : Interactable.Base.Interactable
{
    [Header("Pickable")]
    [SerializeField] protected bool picked;
    [SerializeField] private float _pickDuration;
    [SerializeField] protected float dropDuration;
    
    // Start is called before the first frame update
    protected void Awake()
    {
        _pickDuration = .5f;
        dropDuration = .5f;
        SetPicked(false); 
    }

    public virtual IEnumerator Pick(Responsible responsible)
    {
        yield return StartCoroutine(responsible.Walk(interactionPoint.transform.position));
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), _pickDuration);
        responsible.Inventory.Add(this.gameObject);
        SetPicked(true);
        responsible.FinishJob();
    }
    
    public virtual IEnumerator Drop(Responsible responsible)
    {
        yield return Util.WaitForSeconds(responsible.GetCurrentJob(), dropDuration);
        responsible.Inventory.Remove(gameObject);
        gameObject.transform.position = responsible.directionPosition.transform.position;
        SetPicked(false);
        responsible.FinishJob();
    }

    public void SetPicked(bool pickedStatus)
    {
        Debug.Log("Inside!!!");
        if (pickedStatus)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Methods.Remove(GetType().GetMethod("Pick"));
            Methods.Add(GetType().GetMethod("Drop"));
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Methods.Remove(GetType().GetMethod("Drop"));
            Methods.Add(GetType().GetMethod("Pick"));
        }

        picked = pickedStatus;
    }
}
