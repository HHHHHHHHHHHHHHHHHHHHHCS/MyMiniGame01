using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static KeyCode jumpKey = KeyCode.Space;

    public static float distanceTraveled;

    public float gameOverY = -6;
    public float acceleration = 5;
    public Vector3 jumpVelocity = new Vector3(1, 7, 0);

    private bool touchingPlatform;
    private Rigidbody rigi;
    private Renderer render;
    private Vector3 startPosition;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        render.enabled = false;
        rigi.isKinematic = true;
        enabled = false;
    }

    private void Update()
    {
        if (touchingPlatform && Input.GetKeyDown(jumpKey))
        {
            rigi.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatform = false;
        }

        distanceTraveled = transform.localPosition.x;
        if (transform.localPosition.y < gameOverY)
        {
            GameEventManager.TriggerGameOver();
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

    private void GameStart()
    {
        distanceTraveled = 0;
        transform.localPosition = startPosition;
        render.enabled = true;
        rigi.isKinematic = false;
        enabled = true;

    }

    private void GameOver()
    {
        render.enabled = false;
        rigi.isKinematic = true;
        enabled = false;
    }
}