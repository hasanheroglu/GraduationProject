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
            Debug.Log("Hit damagable!");
            if (damagable.GetComponent<IDamagable>().Damage(damage))
            {
                var killQuests = QuestManager.FindWithActivityType(responsible, ActivityType.Kill);
                foreach (var quest in killQuests)
                {
                    quest.Progress(ActivityType.Kill, damagable.GetComponent<Interactable.Base.Interactable>().GetGroupName());
                }
                UIManager.Instance.SetQuests(responsible);
            }
            damagable.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized*50);
            Destroy(this.gameObject);
        }
    }
}
