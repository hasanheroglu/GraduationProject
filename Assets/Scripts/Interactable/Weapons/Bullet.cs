using System;
using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Interface;
using Manager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Responsible responsible;
    private bool _damaged;
    
    // Start is called before the first frame update
    void Start()
    {
        _damaged = false;
        Destroy(this.gameObject, 1.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null) return;
        
        var damagable = Util.GetDamagableFromCollider(other);
        
        if (damagable != null && !_damaged)
        {
            _damaged = true;
            if (damagable.GetComponent<IDamagable>().Damage(damage))
            {
                DamageUtil.ProgressKillQuests(responsible, damagable);   
            }
            damagable.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized*50);
            Destroy(this.gameObject);
        }
    }
    
}
