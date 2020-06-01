using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZombieFactory
{
    private static readonly GameObject Zombie = Resources.Load<GameObject>("Prefabs/Interactables/Creatures/Zombie");
    
    
    public static GameObject GetZombie(Vector3 position)
    {
        var zombie = Object.Instantiate(Zombie, position, Zombie.transform.rotation);
        return zombie;
    }
}
