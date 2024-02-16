using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    
    public static float bestTime { get; private set; } = float.MaxValue;
    public float currentTime { get; private set; } = 0;

    public Text timeText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeText = GetComponent<Text>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        timeText.text = "Time: " + currentTime.ToString("F2") + " sec.";
    }

    public void CheckHighScore()
    {
        if (currentTime < bestTime) 
            bestTime = currentTime;
    }
}
