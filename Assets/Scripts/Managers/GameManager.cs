using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameActive { get; private set; }

    private void Start()
    {
        this.Init();
        SpawnManager.Instance.Init();
        UIManager.Instance.Init();
        ScoreManager.Instance.Init();

        StartGame();
    }

    // Initializes the singleton.
    private void Init()
    {

    }

    // Starts the game
    private void StartGame()
    {
        IsGameActive = true;
    }

    // Ends the game
    private void EndGame()
    {
        IsGameActive = false;
    }
}
