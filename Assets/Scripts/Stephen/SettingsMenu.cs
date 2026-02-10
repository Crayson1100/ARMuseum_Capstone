using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Unity.Hierarchy;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Main UI")]
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Camera mainCamera;

    [Header("Audio")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    [Header("Font")]
    [SerializeField] Slider fontSlider;
    [SerializeField] TMP_Text fontText;
    [SerializeField] TMP_Text[] allUIText;

    float defaultOrthoSize = 5f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }
    //void Awake()
    //{
    //    masterSlider.value = 50f;
    //    musicSlider.value = 50f;
    //    soundSlider.value = 50f;
    //}
    public void ToggleSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
    public void ZoomIn()
    {
        mainCamera.orthographicSize -= 1f;
    }
    public void ZoomOut()
    {
        mainCamera.orthographicSize += 1f;
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetSoundVolume();
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void ToggleMuteMaster()
    {
        mixer.SetFloat("MasterVolume", -80f);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void ToggleMuteMusic()
    {
        mixer.SetFloat("MusicVolume", -80f);
    }
    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void ToggleMuteSound()
    {
        mixer.SetFloat("SFXVolume", -80f);
    }

    public void SetFontSize()
    {
        float size = fontSlider.value;

        foreach (TMP_Text text in allUIText)
        {
            text.fontSize = size;
        }

        fontText.text = "A" + size.ToString("0");
    }
    public void Exit()
    {
        settingsMenu.SetActive(false);
    }
}

