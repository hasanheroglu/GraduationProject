using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private float _time;
    private int _hour;
    private int _minute;
    private int _temperature;

    [SerializeField] private Light sun;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        _hour = 0;
        _minute = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetTime();
        
        if(Input.GetKeyDown(KeyCode.Alpha1)) SetSpeedNormal();
        if(Input.GetKeyDown(KeyCode.Alpha2)) SetSpeedFaster();
        if(Input.GetKeyDown(KeyCode.Alpha3)) SetSpeedFastest();
    }

    public static void SetSpeedNormal()
    {
        Time.timeScale = 1;
    }

    public static void SetSpeedFaster()
    {
        Time.timeScale = 2;
    }

    public static void SetSpeedFastest()
    {
        Time.timeScale = 4;
    }

    private void GetTime()
    {
        _time += Time.deltaTime;

        if (_time > 5)
        {
            _time = 0;
            _minute++;
            RotateSun();
        }

        if (_minute == 60)
        {
            _minute = 0;
            _hour++;
        }

        if (_hour == 24)
        {
            _hour = 0;
        }
    }

    private void GetWeather()
    {
        
    }

    private void RotateSun()
    {
        sun.transform.RotateAround(Vector3.zero, Vector3.forward, 0.6f);
    }
    
    
}
