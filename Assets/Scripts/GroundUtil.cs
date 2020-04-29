using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Interactable.Environment;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public static class GroundUtil
{
    public static void Clear(Vector3 position)
    {
        var ground = FindGround(position);

        if (ground != null)
        {
            ground.Occupied = false;
        }
    }
    
    public static void Occupy(Vector3 position)
    {
        var ground = FindGround(position);

        if (ground != null)
        {
            ground.Occupied = true;
        }
    }

    public static void CheckOccupied(Vector3 position)
    {
        
    }

    public static Ground FindGround(Vector3 position)
    {
        var colliders = Physics.OverlapBox(position, new Vector3(0.1f, 10f, 0.1f));
        Ground ground = null;
        foreach (var collider in colliders)
        {
            ground = Util.GetGroundFromCollider(collider);
            if (ground != null)
            {
                break;
            }
        }

        return ground;
    }
}
