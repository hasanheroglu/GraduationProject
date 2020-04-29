using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject _parent;
    private Responsible _responsible;
    private float _baseScale;
    
    // Start is called before the first frame update
    void Start()
    {
        _parent = gameObject.transform.parent.gameObject;
        _responsible = _parent.GetComponent<Responsible>();
        _baseScale = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Camera.main.transform.localRotation.eulerAngles.y - transform.parent.localRotation.eulerAngles.y, 0);
        transform.localScale = new Vector3(((float)_responsible.health/100)*_baseScale, 1, 1);
    }
}
