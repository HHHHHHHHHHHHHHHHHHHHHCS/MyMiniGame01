using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGameManager : MonoBehaviour
{
    public Maze mazePrefab; //todo:
    public MazePlayer playerPrefab;

    private Maze mazeInstance;
    private MazePlayer playerInstance;

    private void Start()
    {
        StartCoroutine(BeginGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private IEnumerator BeginGame()
    {
        Camera mainCam = Camera.main;

        mainCam.clearFlags = CameraClearFlags.Skybox;
        mainCam.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate(mazePrefab);
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab);
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        mainCam.clearFlags = CameraClearFlags.Depth;
        mainCam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }

        StartCoroutine(BeginGame());
    }
}