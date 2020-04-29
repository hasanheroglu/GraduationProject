using System;
using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private bool _damaged;
    
    // Start is called before the first frame update
    void Start()
    {
        _damaged = false;
        Destroy(this.gameObject, 1.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var damagable = Util.GetDamagableFromCollider(other);
        
        if (damagable != null && !_damaged)
        {
            _damaged = true;
            Debug.Log("Hit damagable!");
            damagable.GetComponent<IDamagable>().Damage(damage);
            damagable.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized*50);
            Destroy(this.gameObject);
        }
    }
}
