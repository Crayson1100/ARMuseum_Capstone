using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

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

    void Awake()
    {
        // Set default slider values
        masterSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        soundSlider.value = 0.5f;

        // Apply volumes to mixer
        SetMasterVolume();
        SetMusicVolume();
        SetSoundVolume();
    }
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
    public void SetMasterVolume()
    {
        float value = masterSlider.value;
        mixer.SetFloat("Master Volume", Mathf.Log10(value) * 20);
    }
    public void ToggleMuteMaster()
    {
        mixer.SetFloat("Master Volume", -80f);
    }
    public void SetMusicVolume()
    {
        float value = musicSlider.value;
        mixer.SetFloat("Music Volume", Mathf.Log10(value) * 20);
    }
    public void ToggleMuteMusic()
    {
        mixer.SetFloat("Music Volume", -80f);
    }
    public void SetSoundVolume()
    {
        float value = soundSlider.value;
        mixer.SetFloat("SFX Volume", Mathf.Log10(value) * 20);
    }

    public void ToggleMuteSound()
    {
        mixer.SetFloat("SFX Volume", -80f);
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

