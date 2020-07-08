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
    }

    public void SetColor(Color color)
    {
        if (!_renderer || _renderer.materials == null) return;
        
        foreach (var material in _renderer.materials)
        {
            material.SetColor("_EmissionColor", material.color * color);
            material.EnableKeyword("_EMISSION");
        }
    }

    public void OnMouseEnter()
    {
        if (!_renderer || _renderer.materials == null) return;

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
