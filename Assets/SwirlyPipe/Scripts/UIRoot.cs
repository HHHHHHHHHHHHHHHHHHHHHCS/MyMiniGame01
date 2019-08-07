using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    public Player player;

    public Transform startGame, endGame, hudGame;

    private Text endScoreText, nowScoreText, nowSpeedText;

    private void Awake()
    {
        startGame.localScale = Vector3.one;
        endGame.localScale = Vector3.zero;
        hudGame.localScale = Vector3.zero;

        AddOnClick(startGame, "Speed_One", () => StartGame(1));
        AddOnClick(startGame, "Speed_One", () => StartGame(2));
        AddOnClick(startGame, "Speed_One", () => StartGame(3));

        endScoreText = endGame.Find("ScoreText").GetComponent<Text>();
        AddOnClick(endGame, "BackButton", GotoStartGame);
    }

    private void StartGame(int speed)
    {
        startGame.localScale = Vector3.zero;
        hudGame.localScale = Vector3.one;

        player.StartGame();
    }

    public void EndGame(float distance)
    {
        endScoreText.text = ((int) (distance * 10f)).ToString();
        hudGame.localScale = Vector3.zero;
        endGame.localScale = Vector3.one;
    }

    private void GotoStartGame()
    {
        endGame.localScale = Vector3.zero;
        startGame.localScale = Vector3.one;
    }

    private void AddOnClick(Transform parent, string path, UnityAction act)
    {
        parent.Find(path).GetComponent<Button>().onClick.AddListener(act);
    }
}