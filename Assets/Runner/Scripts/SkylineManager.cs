﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SkylineManager : MonoBehaviour
{
    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;
    public Vector3 startPosition;
    public Vector3 minSize, maxSize;


    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;

        objectQueue = new Queue<Transform>(numberOfObjects);

        for (int i = 0; i < numberOfObjects; i++)
        {
            objectQueue.Enqueue(Instantiate(prefab));
        }

        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
    }

    private void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.Instance.distanceTraveled)
        {
            Recycle();
        }
    }

    private void Recycle()
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize.x, maxSize.y),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z));

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;
        Transform o = objectQueue.Dequeue();
        o.localScale = scale;
        o.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(o);
    }

    private void GameStart()
    {
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }

        enabled = true;
    }

    private void GameOver()
    {
        enabled = false;
    }
}