using System;
using System.Collections;
using System.Collections.Generic;
using Attribute;
using Interactable.Base;
using Interactable.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : Pickable, IPlaceable
{
    private static int _instanceCount;
    private bool _placed;
    
    private void Awake()
    {
        instanceNo = _instanceCount;
        _instanceCount++;
        InUse = 1;
        _placed = false;
        SetMethods();
        base.Awake();
    }

    
    [Interactable(typeof(Responsible))]
    public override IEnumerator Pick(Responsible responsible)
    {
        return base.Pick(responsible);
    }

    [Interactable(typeof(Responsible))]
    public override IEnumerator Drop(Responsible responsible)
    {
        return base.Drop(responsible);
    }

    [Interactable(typeof(Responsible))]
    [Activity(ActivityType.Place)]
    public IEnumerator Place(Responsible responsible)
    {
        if (_placed)
        {
            GroundUtil.Clear(transform.position);
        }

        Time.timeScale = 0;
        while (!Input.GetMouseButtonDown(0))
        {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                float x = hit.point.x % 2;
                if (x > 1) x = Mathf.Ceil(hit.point.x);
                else x = Mathf.Floor(hit.point.x);
                
                float z = hit.point.z % 2;
                if (z > 1) z = Mathf.Ceil(hit.point.z);
                else z = Mathf.Floor(hit.point.z);

                /*
                var  rand  = Random.insideUnitCircle * 5f;
                rand.x += responsible.gameObject.transform.position.x;
                rand.y += responsible.gameObject.transform.position.z;

                if (x <= rand.x && z <= rand.y)
                {
                    gameObject.transform.position = new Vector3(x, gameObject.transform.localScale.y/2, z);
                }
                */
                gameObject.transform.position = new Vector3(x, gameObject.transform.localScale.y/2, z);
                var ground_ = GroundUtil.FindGround(transform.position);
                if (ground_ != null)
                {
                    if (ground_.occupied)
                    {
                        gameObject.GetComponent<ShaderAdjuster>().SetColor(Color.red);
                    }
                    else
                    {
                        gameObject.GetComponent<ShaderAdjuster>().SetColor(Color.green);
                    }
                }
                
                gameObject.transform.rotation = Quaternion.identity;
            }
            yield return null;
        }
        _placed = true;
        yield return new WaitUntil(() => _placed);
        Time.timeScale = 1;
        var ground = GroundUtil.FindGround(transform.position);
        if (!ground.occupied)
        {
            GroundUtil.Occupy(gameObject.transform.position);
            SetPicked(!_placed);
            responsible.Inventory.Remove(GetGroupName());
        }
        else
        {
            transform.position = new Vector3(0, -100f, 0);
        }
        
        responsible.FinishJob();
    }
}
