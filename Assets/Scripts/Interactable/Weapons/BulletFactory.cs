using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public static class BulletFactory
{
    private static readonly GameObject PistolBullet = Resources.Load<GameObject>("Prefabs/PistolBullet");
    private static readonly GameObject ShotgunBullet = Resources.Load<GameObject>("Prefabs/ShotgunBullet");
    private static readonly GameObject Bullet = Resources.Load<GameObject>("Prefabs/Bullet");

    public static GameObject GetBullet(Vector3 position, Responsible responsible, int damage)
    {
        var bullet = Object.Instantiate(Bullet, position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().responsible = responsible;
        Bullet.GetComponent<Bullet>().damage = damage;
        return bullet;
    }

    public static GameObject GetPistolBullet(Vector3 position, int damage)
    {
        var bullet = Object.Instantiate(PistolBullet, position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().damage = damage;
        return bullet;
    }

    public static GameObject GetShotgunBullet(Vector3 position, int damage)
    {
        var bullet = Object.Instantiate(ShotgunBullet, position, Quaternion.identity);
        Bullet.GetComponent<Bullet>().damage = damage;
        return bullet;
    }
}

