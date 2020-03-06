using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    private Responsible _responsible;
    
    // Start is called before the first frame update
    void Start()
    {
        _responsible = _parent.GetComponent<Responsible>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(((float)_responsible.health/100)*10, 1, 1);
    }
}
