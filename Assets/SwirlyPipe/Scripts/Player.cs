using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UIRoot uiRoot;
    public PipeSystem pipeSystem;
    public float velocity;
    public float rotationVelocity;

    private Pipe currentPipe;
    private float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private float worldRotation,avatarRotation;
    private Transform world,rotater;


    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        UpdateAvatarRotation();
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }

        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation += rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }
        rotater.localRotation = Quaternion.Euler(avatarRotation,0f,0f);
    }

    public void Die()
    {
        uiRoot.EndGame(distanceTraveled);
        gameObject.SetActive(false);
    }
}