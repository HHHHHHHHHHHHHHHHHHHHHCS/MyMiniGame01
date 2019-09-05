using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public ParticleSystem shape, trail, burst;

    public float deathCountDown = -1f;

    private SwirlyPipePlayer player;

    private void Awake()
    {
        player = transform.root.GetComponent<SwirlyPipePlayer>();
    }

    private void Update()
    {
        if (deathCountDown >= 0f)
        {
            deathCountDown -= Time.deltaTime;
            if (deathCountDown <= 0f)
            {
                deathCountDown = -1f;
                var temp = shape.emission;
                temp.enabled = true;
                temp = trail.emission;
                temp.enabled = true;
                player.Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (deathCountDown < 0f)
        {
            var temp = shape.emission;
            temp.enabled = false;
            temp = trail.emission;
            temp.enabled = false;
            burst.Emit(burst.main.maxParticles);
            deathCountDown = burst.main.startLifetimeMultiplier;
        }
    }
}