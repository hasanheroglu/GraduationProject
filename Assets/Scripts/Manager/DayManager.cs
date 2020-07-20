using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct Clock
{
    public int Hour;
    public int Minute;
};

public class DayManager : MonoBehaviour
{
    private float _time;
    private Clock _clock;
    private int _temperature;
    private float _sunRotateAmount;
    private static GameObject _gameSpeedText;

    [SerializeField] private int gameMinuteInRealtimeSeconds = 1;
    [SerializeField] private Light sun;
    [SerializeField] private GameObject clock;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        _clock.Hour = 12;
        _clock.Minute = 0;
        _sunRotateAmount = 0.25f; //180 degrees divided by 12 (12 hours day / 12 hours night) and divided by minutes in an hour
        _gameSpeedText = GameObject.Find("Canvas").transform.Find("GameSpeed").gameObject;
        SetSpeedNormal();
    }

    // Update is called once per frame
    void Update()
    {
        GetTime();
        SetClock();
        
        if(Input.GetKeyDown(KeyCode.DoubleQuote)) SetSpeedZero();
        if(Input.GetKeyDown(KeyCode.Alpha1)) SetSpeedNormal();
        if(Input.GetKeyDown(KeyCode.Alpha2)) SetSpeedFaster();
        if(Input.GetKeyDown(KeyCode.Alpha3)) SetSpeedFastest();
    }

    public static void SetSpeedZero()
    {
        Time.timeScale = 0;
        _gameSpeedText.GetComponent<Text>().text = "Game Speed: Paused";
    }

    public static void SetSpeedNormal()
    {
        Time.timeScale = 1;
        _gameSpeedText.GetComponent<Text>().text = "Game Speed: 1x";
    }

    public static void SetSpeedFaster()
    {
        Time.timeScale = 2;
        _gameSpeedText.GetComponent<Text>().text = "Game Speed: 2x";
    }

    public static void SetSpeedFastest()
    {
        Time.timeScale = 4;
        _gameSpeedText.GetComponent<Text>().text = "Game Speed: 4x";
    }

    private void GetTime()
    {
        _time += Time.deltaTime;

        if (_time >= gameMinuteInRealtimeSeconds)
        {
            _time = 0;
            _clock.Minute++;
            RotateSun();
        }

        if (_clock.Minute == 60)
        {
            _clock.Minute = 0;
            _clock.Hour++;
        }

        if (_clock.Hour == 24)
        {
            _clock.Hour = 0;
        }
    }

    private void SetClock()
    {
        if (clock == null) return;
    
        clock.GetComponent<Text>().text  = String.Format("{0:D2}:{1:D2}", _clock.Hour, _clock.Minute);
    }

    private void GetWeather()
    {
        
    }

    private void RotateSun()
    {
        sun.transform.RotateAround(Vector3.zero, Vector3.forward, _sunRotateAmount);
    }
    
    
}
