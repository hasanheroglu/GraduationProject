using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; set; }
    
    public Texture2D map;
    public GameObject groundPrefab;
    public ColorToPrefab[] colorMappings;
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        GenerateMap();
    }

    public void BuildNavMesh()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void UpdateNavMesh()
    {
        GetComponent<NavMeshSurface>().UpdateNavMesh(GetComponent<NavMeshSurface>().navMeshData);
    }

    public void GenerateMap()
    {
        GenerateGround();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateObject(x, y);
            }
        }
    }

    private void GenerateGround()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                var positioner = new Vector3(x, 1, y);
                Instantiate(groundPrefab, Vector3.Scale(positioner, new Vector3(2f, 0f, 2f)), Quaternion.identity, transform);
            }
        }
    }
    
    private void GenerateObject(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        var positioner = new Vector3(x, 1, y);
        
        foreach (var colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                var go = Instantiate(colorMapping.prefab, Vector3.Scale(positioner, colorMapping.position), colorMapping.prefab.transform.rotation, transform);
                
                if(colorMapping.width == 0 || colorMapping.length == 0) continue;
                    
                var colliders = Physics.OverlapBox(go.transform.position, new Vector3(colorMapping.width, 10, colorMapping.length) * 0.9f);
                
                foreach (var collider in colliders)
                {
                    var ground = Util.GetGroundFromCollider(collider);
                    
                    if(ground == null) continue;
                    
                    ground.occupied = true;
                }
            }
        }
    }
}
