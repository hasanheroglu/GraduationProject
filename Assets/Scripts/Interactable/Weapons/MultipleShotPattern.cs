using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleShotPattern : WeaponPattern
{
    public MultipleShotPattern()
    {

    }
    
    public override void Use(GameObject weapon, Vector3 direction)
    {
        if (!weapon.GetComponent<Weapon>().CanUse) return;

        var bulletPosition = weapon.GetComponent<Weapon>().bulletPoint.transform.position;
        var responsible = weapon.GetComponent<Weapon>().Responsible;
        var damage = weapon.GetComponent<Weapon>().damage;
        var range = weapon.GetComponent<Weapon>().range;
        
        var bulletGo1 = BulletFactory.GetBullet(bulletPosition, responsible, damage);
        var bulletGo2 = BulletFactory.GetBullet(bulletPosition, responsible, damage);
        var bulletGo3 = BulletFactory.GetBullet(bulletPosition, responsible, damage);
        var bulletGo4 = BulletFactory.GetBullet(bulletPosition, responsible, damage);
        var bulletGo5 = BulletFactory.GetBullet(bulletPosition, responsible, damage);

        bulletGo1.GetComponent<Rigidbody>().velocity = direction.normalized * range;
        bulletGo2.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -30, 0) * direction.normalized * range;
        bulletGo3.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 30, 0) * direction.normalized * range;
        bulletGo4.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -15, 0) * direction.normalized * range;
        bulletGo5.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 15, 0) * direction.normalized * range;
    }
}
