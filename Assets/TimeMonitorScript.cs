using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMonitorScript : MonoBehaviour
{
    private Text clock;

    private int hours = 0;
    private int minutes = 0;

    private int frames = 0;
    public int UPDATE_RATE = 60;

    // Start is called before the first frame update
    public void Start()
    {
        clock = GetComponentInChildren<Text>();
        clock.text = $"{hours.ToString("00")}:{minutes.ToString("00")}";
    }

    // Update is called once per frame
    public void Update()
    {
        frames++;

        if (frames != UPDATE_RATE)
        {
            return;
        }

        frames = 0;

        minutes++;
        if (minutes == 60)
        {
            hours++;
            minutes = 0;
            if (hours == 24)
            {
                hours = 0;
            }
        }
        clock.text = $"{hours.ToString("00")}:{minutes.ToString("00")}";
    }

    public int GetHours()
    {
        return hours;
    }
}
