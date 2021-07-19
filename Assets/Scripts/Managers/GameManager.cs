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
    public void StartGame()
    {
        IsGameActive = true;
        Time.timeScale = 1.0f;
    }

    // Ends the game
    public void EndGame()
    {
        ScoreManager.Instance.EndGame();
        IsGameActive = false;
        Time.timeScale = 0.0f;

        UIManager.Instance.ShowDeathMenu();
    }
}
