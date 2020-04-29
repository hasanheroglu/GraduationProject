using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShotPattern : WeaponPattern
{
    public MultipleShotPattern()
    {
        damage = 15;
        range = 5f;
        delay = 2f;
    }
    
    public MultipleShotPattern(int damage, float range, float delay)
    {
        this.damage = damage;
        this.range = range;
        this.delay = delay;
    }
    
    public override void Use(GameObject weapon, Vector3 direction)
    {
        var position = weapon.transform.position;

        var bulletGo1 = BulletFactory.GetBullet(position + direction * 0.05f, damage);
        var bulletGo2 = BulletFactory.GetBullet(position + direction * 0.05f, damage);
        var bulletGo3 = BulletFactory.GetBullet(position + direction * 0.05f, damage);
        var bulletGo4 = BulletFactory.GetBullet(position + direction * 0.05f, damage);
        var bulletGo5 = BulletFactory.GetBullet(position + direction * 0.05f, damage);

        bulletGo1.GetComponent<Rigidbody>().velocity = direction.normalized * range;
        bulletGo2.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -30, 0) * direction.normalized * range;
        bulletGo3.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 30, 0) * direction.normalized * range;
        bulletGo4.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -15, 0) * direction.normalized * range;
        bulletGo5.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 15, 0) * direction.normalized * range;
    }
}
