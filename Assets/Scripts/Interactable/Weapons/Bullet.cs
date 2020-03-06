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
        damage = 50;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Damagable"))
        {
            Debug.Log("Hit damagable!");
            other.gameObject.GetComponentInParent<Interactable.Base.Interactable>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
