using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    // Settings UI components
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle fullscreenToggle;

    // Settings variables
    [SerializeField]
    private AudioMixer audioMixer;
    private Resolution[] resolutions;

    void Start()
    {
        // Init variables
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        // Clear current resolution options
        resolutionDropdown.ClearOptions();

        // Find all possible resolutions
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // If the found resolution is the user's current resolution, store it
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add back resolution options
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Init quality to current quality setting
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // Init current volume to slider default
        SetVolume(volumeSlider.value);

        // Init fullscreen toggle to current window
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    // Sets the resolution of the game window
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Sets the volume of the game
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    // Sets the graphics quality of the game
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Sets the game game window to be fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
