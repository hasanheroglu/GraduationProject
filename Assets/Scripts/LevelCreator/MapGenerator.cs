using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public Texture2D map;
    public GameObject groundPrefab;
    public ColorToPrefab[] colorMappings;
    
    void Start()
    {
        GenerateMap();
        GetComponent<NavMeshSurface>().BuildNavMesh();            
    }

    private void GenerateMap()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateObject(x, y);
            }
        }
    }

    private void GenerateObject(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        Instantiate(groundPrefab, new Vector3(x * 1.5f, 0f, y * 1.5f), Quaternion.identity, transform);
        
        foreach (var colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (pixelColor == Color.black) //For flower
                {
                    Debug.Log("Creating a flower.");
                    Instantiate(colorMapping.prefab, new Vector3(x * 1.5f, 0.15f, y * 1.5f), Quaternion.identity, transform);
                } else if (pixelColor == Color.green) //For tree
                {
                    Debug.Log("Creating a tree.");
                    Instantiate(colorMapping.prefab, new Vector3(x * 1.5f, 0.28f, y * 1.5f), Quaternion.identity, transform);
                }
            }
        }
    }
    
}
