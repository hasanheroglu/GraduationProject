using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damagable = Util.GetDamagableFromCollider(other);
        
        if (damagable != null)
        {
            Debug.Log("Hit damagable!");
            damagable.Damage(damage);
            //damagable.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity*20);
            Destroy(this.gameObject);
        }
    }
}
