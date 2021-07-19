using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private int highscore = 0;
    private string filePath = "";

    // Custom game save class
    [System.Serializable]
    public class GameSave
    {
        public GameSave(int _highscore) { highscore = _highscore; }
        public int highscore { get; set; }
    }

    // Initializes the singleton. Should only be called from GameManager.
    public void Init()
    {
        // Set persistent file path
        filePath = Application.persistentDataPath + "/save_game.dat";

        // Load old highscore
        GameSave tempSave = LoadGame();
        if (tempSave != null)
            highscore = tempSave.highscore;

        currentScore = 0;
        UpdateScoreDisplay();
    }

    // Ends the game
    public void EndGame()
    {
        // If player reached a new highscore, save it
        if(currentScore > highscore)
        {
            highscore = currentScore;
        }

        // Reset score
        currentScore = 0;
        UpdateScoreDisplay();
        SaveGame();
    }

    // Saves the current game to file
    public void SaveGame()
    {
        SaveGame(new GameSave(highscore));
    }

    // Saves the given game to file
    private void SaveGame(GameSave gameSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, gameSave);
        file.Close();
    }

    // Loads the game
    private GameSave LoadGame()
    {
        // If file for saveGame exists, load it
        if(File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            GameSave gameSave = (GameSave)bf.Deserialize(file);
            file.Close();

            UpdateScoreDisplay();
            return gameSave;
        }
        // Else, return null
        else
        {
            Debug.Log($"File doesn't exist at path: {filePath}");
            return null;
        }
    }

    // Updates the score display in the UI
    public void UpdateScoreDisplay()
    {
        // TODO: Update UI
    }

    // Getters and setters
    public int GetCurrentScore() { return currentScore; }
    public int GetCurrentHighscore() { return highscore; }

    // Wrapper functions to enable modifying player scores. Also update UI display
    public void AddScore(int _score) { SetScore(currentScore + _score); }
    public void SubScore(int _score) { SetScore(currentScore - _score); }
    public void SetScore(int _score)
    {
        currentScore = _score;
        UpdateScoreDisplay();
    }
}
