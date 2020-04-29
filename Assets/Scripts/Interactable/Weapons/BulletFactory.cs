using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletFactory
{
    private static readonly GameObject Bullet = Resources.Load<GameObject>("Prefabs/Bullet");

    public static GameObject GetBullet(Vector3 position, int damage)
    {
        var bullet = Object.Instantiate(Bullet, position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().damage = damage;
        return bullet;
    }
}
