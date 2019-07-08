using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text gameOverText, instructionsText, runnerText, distanceText, boostsText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameOverText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Runner.Instance.jumpKey))
        {
            GameEventManager.TriggerGameStart();
        }
    }

    private void GameStart()
    {
        gameOverText.enabled = false;
        instructionsText.enabled = false;
        runnerText.enabled = false;
        enabled = false;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        instructionsText.enabled = true;
        enabled = true;
    }

    public void SetBoosts(int boosts)
    {
        boostsText.text = boosts.ToString();
    }

    public void SetDistance(float distance)
    {
        distanceText.text = distance.ToString("f0");
    }
}