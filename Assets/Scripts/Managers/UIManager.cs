using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    // Main UI components
    [SerializeField]
    private GameObject hud;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject deathMenu;

    // HUD UI components
    [SerializeField]
    private Slider energySlider;
    [SerializeField]
    private Slider cooldownSlider;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;

    // Death Menu UI components
    [SerializeField]
    private TextMeshProUGUI deathScoreText;

    // EventSystem selection components
    [SerializeField]
    private GameObject hudDefaultSelect = null;
    [SerializeField]
    private GameObject pauseMenuDefaultSelect;
    [SerializeField]
    private GameObject deathMenuDefaultSelect;

    // Initializes the singleton. Should only be called from GameManager.
    public void Init()
    {
        ShowHUD();
    }

    // Shows the HUD
    public void ShowHUD()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        hud.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hudDefaultSelect);
    }

    // Shows the death menu
    public void ShowDeathMenu()
    {
        hud.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(deathMenuDefaultSelect);
    }

    // Shows the pause menu
    public void ShowPauseMenu()
    {
        hud.SetActive(false);
        deathMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuDefaultSelect);
    }

    // Toggles the pause state
    public void TogglePause()
    {
        if (Time.timeScale != 0.0f)
            PauseGame();
        else
            ResumeGame();
    }

    // Pauses the game
    public void PauseGame()
    {
        ShowPauseMenu();
        Time.timeScale = 0.0f;
    }

    // Resumes the game
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        ShowHUD();
    }

    // Updates the energy slider to display the given value
    public void UpdateEnergySlider(float value)
    {
        energySlider.value = value;
    }

    // Updates the cooldown slider to display the given value
    public void UpdateCooldownSlider(float value)
    {
        cooldownSlider.value = value;
    }

    // Updates the score display
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score:\n" + score.ToString();
    }

    // Updates the score display
    public void UpdateHighscoreText(int score)
    {
        highscoreText.text = "Highscore:\n" + score.ToString();
    }

    // Sets the displaying of the player scores in the death menu
    public void SetDeathScore(int curScore, int highscore)
    {
        deathScoreText.text = "Score: " + curScore.ToString() + "\nHighscore: " + highscore.ToString();
    }
}
