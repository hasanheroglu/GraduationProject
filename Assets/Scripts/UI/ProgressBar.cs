using System.Collections;
using System.Collections.Generic;
using Interactable.Base;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image _image;    
    private float _baseScale;
    private float _duration;
    private float _startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _baseScale = 5f;
        _duration = 0f;
        _startTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _startTime > _duration) _duration = 0;

        if(_duration > 0)
            _image.fillAmount = ((float) (Time.time - _startTime) / _duration);
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
        _startTime = Time.time;
    }
}
