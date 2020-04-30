using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory
{
    private static readonly GameObject Table = Resources.Load<GameObject>("Prefabs/Interactables/Items/Table");

    public static GameObject GetTable(Vector3 position)
    {
        var table = GameObject.Instantiate(Table, position, Quaternion.identity);
        return table;
    }
}
