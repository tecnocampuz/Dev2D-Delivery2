using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{
    public static DistanceController instance;
    public float currentDistance { get; private set; } = 0;
    
    public Text distanceText;

    public Transform player;
    private Vector3 lastPosition;
    
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        distanceText = GetComponent<Text>();
        distanceText.text = "Distance: " + currentDistance.ToString();

        if (player != null)
            lastPosition = player.position;
    }
    
    void Update()
    {
        if (player != null)
        {
            currentDistance += Vector3.Distance(player.position, lastPosition);
            lastPosition = player.position;
            
            distanceText.text = "Distance: " + currentDistance.ToString("F2") + " m.";
        }
    }
}
