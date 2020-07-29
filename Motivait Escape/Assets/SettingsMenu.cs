using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider audioVolumeSlider;
    public Button applyButton;

    public AudioSource audioSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    private void OnEnable()
    {
        gameSettings = new GameSettings();
        fullscreenToggle.onValueChanged.AddListener(delegate { FullScreenToggle(); } );
        resolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { ChangeTextureQuality(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { ChangeAntiAliasing(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { ToggleVSync(); });
        audioVolumeSlider.onValueChanged.AddListener(delegate { ChangeMasterVolume(); });
        applyButton.onClick.AddListener(delegate { ApplyButtonPressed(); });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();
    }
    public void FullScreenToggle()
    {
        gameSettings.isFullScreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    void ChangeResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void ChangeTextureQuality() 
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    void ChangeAntiAliasing()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
        gameSettings.antialisasing = antialiasingDropdown.value;
    }
    void ToggleVSync()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void ChangeMasterVolume()
    {
        audioSource.volume = gameSettings.audioVolume = audioVolumeSlider.value;
    }

    public void ApplyButtonPressed()
    {
        SaveSettings();
    }
    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings,true);
        File.WriteAllText(Application.persistentDataPath + "/gamesetting.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesetting.json"));
        audioVolumeSlider.value = gameSettings.audioVolume;
        antialiasingDropdown.value = gameSettings.antialisasing;
        vSyncDropdown.value = gameSettings.vSync;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        textureQualityDropdown.value = gameSettings.textureQuality;
        fullscreenToggle.isOn = gameSettings.isFullScreen;

        resolutionDropdown.RefreshShownValue();
    }
}
