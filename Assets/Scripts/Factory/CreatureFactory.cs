using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Interactable.Base;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public static class CreatureFactory
{
    private static readonly GameObject Cow = Resources.Load<GameObject>("Prefabs/Interactables/Creatures/Animals/Cow");
    private static readonly GameObject Zombie = Resources.Load<GameObject>("Prefabs/Interactables/Creatures/Zombie");


    public static GameObject GetCow(Vector3 position)
    {
        var cow = Object.Instantiate(Cow, position, Cow.transform.rotation);
        return cow;
    }

    public static GameObject GetZombie(Vector3 position)
    {
        var zombie = Object.Instantiate(Zombie, position, Zombie.transform.rotation);
        return zombie;
    }
}
