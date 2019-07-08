using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager Instance;

    public ParticleSystem boostEmitter;
    public ParticleSystem[] particleSystems;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        GameOver();
    }

    public void DoBoost()
    {
        StartCoroutine(PlayBoostEmitter());
    }

    private IEnumerator PlayBoostEmitter()
    {
        boostEmitter.Play();
        yield return new WaitForSeconds(1f);
        boostEmitter.Stop();
    }

    private void GameStart()
    {
        boostEmitter.Clear();
        boostEmitter.Stop();
        var temp = boostEmitter.emission;
        temp.enabled = true;

        foreach (var item in particleSystems)
        {
            item.Clear();
            temp = item.emission;
            temp.enabled = true;
        }
    }

    private void GameOver()
    {
        var temp = boostEmitter.emission;
        temp.enabled = false;

        foreach (var item in particleSystems)
        {
            temp = item.emission;
            temp.enabled = false;
        }
    }
}