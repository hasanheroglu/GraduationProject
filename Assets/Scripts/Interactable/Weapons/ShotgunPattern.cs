using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPattern : WeaponPattern
{
    public ShotgunPattern()
    {
        damage = 15;
        range = 15f;
        delay = 2f;
    }
    
    public override void Use(GameObject weapon, Vector3 direction)
    {
        var position = weapon.transform.position;

        var bulletGo1 = BulletFactory.GetShotgunBullet(position + direction * 0.05f, damage);
        var bulletGo2 = BulletFactory.GetShotgunBullet(position + direction * 0.05f, damage);
        var bulletGo3 = BulletFactory.GetShotgunBullet(position + direction * 0.05f, damage);
        var bulletGo4 = BulletFactory.GetShotgunBullet(position + direction * 0.05f, damage);
        var bulletGo5 = BulletFactory.GetShotgunBullet(position + direction * 0.05f, damage);

        bulletGo1.GetComponent<Rigidbody>().velocity = direction;
        bulletGo2.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -30, 0) * direction;
        bulletGo3.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 30, 0) * direction;
        bulletGo4.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, -15, 0) * direction;
        bulletGo5.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 15, 0) * direction;
        
        Debug.Log("Weapon used shotgun pattern!");
    }
}
