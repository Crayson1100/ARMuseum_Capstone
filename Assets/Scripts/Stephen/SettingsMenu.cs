using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using Unity.Hierarchy;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Main UI")]
    [SerializeField] GameObject advSettingsMenu;
    [SerializeField] GameObject mainSettingsMenu;
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
    private bool isMuted = false;
    private float previousVolume = 0f;

    public void ToggleSettings()
    {
        advSettingsMenu.SetActive(true);
        mainSettingsMenu.SetActive(false);
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
        //musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetSoundVolume();
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void ToggleMuteMaster()
    {
        if (!isMuted)
        {
            mixer.GetFloat("Master", out previousVolume);

            mixer.SetFloat("Master", -80f);
            isMuted = true;
        }
        else
        {
            mixer.SetFloat("Master", previousVolume);
            isMuted = false;
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void ToggleMuteMusic()
    {
        mixer.SetFloat("Music", -80f);
    }
    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void ToggleMuteSound()
    {
        mixer.SetFloat("SFX", -80f);
    }

    public void SetFontSize()
    {
        float size = fontSlider.value;

        foreach (TMP_Text text in allUIText)
        {
            text.fontSize = size;
        }

        fontText.text = "A";
        //fontText.text = "A" + size.ToString("0");

    }
    public void Exit()
    {
        advSettingsMenu.SetActive(false);
        mainSettingsMenu.SetActive(true);
    }
}

