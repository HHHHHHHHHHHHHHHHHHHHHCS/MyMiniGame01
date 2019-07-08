using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static Runner Instance;

    public KeyCode jumpKey = KeyCode.Space;

    public float distanceTraveled;


    private int Boosts
    {
        get => _boosts;
        set
        {
            if (_boosts != value)
            {
                _boosts = value;
                UIManager.Instance.SetBoosts(value);
            }
        }
    }

    public float gameOverY = -6;
    public float acceleration = 5;
    public Vector3 boostVelocity = new Vector3(10, 10, 0);
    public Vector3 jumpVelocity = new Vector3(1, 7, 0);

    private int _boosts;
    private bool touchingPlatform;
    private Rigidbody rigi;
    private Renderer render;
    private Vector3 startPosition;

    private void Awake()
    {
        Instance = this;
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
        if (Input.GetKeyDown(jumpKey))
        {
            if (touchingPlatform)
            {
                rigi.AddForce(jumpVelocity, ForceMode.VelocityChange);
                touchingPlatform = false;
            }
            else if (Boosts > 0)
            {
                rigi.AddForce(boostVelocity, ForceMode.VelocityChange);
                Boosts -= 1;
                UIManager.Instance.SetBoosts(Boosts);
                ParticleSystemManager.Instance.DoBoost();
            }
        }

        distanceTraveled = transform.localPosition.x;
        UIManager.Instance.SetDistance(distanceTraveled);
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
        Boosts = 0;
        UIManager.Instance.SetBoosts(Boosts);
        distanceTraveled = 0;
        UIManager.Instance.SetDistance(distanceTraveled);
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

    public void AddBoost()
    {
        Boosts += 1;
        UIManager.Instance.SetBoosts(Boosts);
    }
}