using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using Manager;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    private GameObject _parent;
    private Responsible _responsible;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        _parent = transform.parent.gameObject;
        _responsible = _parent.GetComponent<Responsible>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionManager.Instance._responsible.Equals(_responsible.gameObject))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        transform.Rotate(Vector3.up, 5f);
    }
}
