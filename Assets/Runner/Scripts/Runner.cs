using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static float distanceTraveled;

    public KeyCode jumpKey = KeyCode.Space;
    public float acceleration = 5;
    public Vector3 jumpVelocity = new Vector3(1, 7, 0);

    private bool touchingPlatform;
    private Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        distanceTraveled = transform.localPosition.x;
        if (touchingPlatform && Input.GetKeyDown(jumpKey))
        {
            rigi.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatform = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchingPlatform)
        {
            rigi.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        touchingPlatform = true;
    }

    private void OnCollisionExit(Collision other)
    {
        touchingPlatform = false;
    }
}