using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rbEnemy;
    [SerializeField]
    public float speed = 3.5f;
    [SerializeField]
    public float angle = 270;

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(speed, 0);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        rbEnemy.velocity = direction;
    }

    void invertMovement(bool targetTouched)
    {
        angle = targetTouched ? angle - 180 : angle + 180;
        angle = (angle + 360) % 360;
    }
}
