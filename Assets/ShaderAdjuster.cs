using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAdjuster : MonoBehaviour
{
    [SerializeField] private GameObject _mesh;
    private Renderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_mesh == null)
        {
            _renderer = GetComponent<Renderer>();
        }
        else
        {
            _renderer = _mesh.GetComponent<Renderer>();
        }
        
        Debug.Log(_renderer.materials[0].color.r);
        
    }

    public void OnMouseEnter()
    {
        foreach (var material in _renderer.materials)
        {
            material.SetColor("_EmissionColor", material.color * Color.gray);
            material.EnableKeyword("_EMISSION");
        }
    }
 
    public void OnMouseExit()
    {
        foreach (var material in _renderer.materials)
        {
            material.DisableKeyword("_EMISSION");
        }
    }
}
