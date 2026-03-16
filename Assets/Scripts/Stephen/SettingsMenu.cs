using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] Slider voiceSlider;

    [Header("Audio Icons")]
    [SerializeField] Image masterIcon;
    [SerializeField] Image musicIcon;
    [SerializeField] Image sfxIcon;
    [SerializeField] Image voiceIcon;

    [SerializeField] Sprite volumeSprite;
    [SerializeField] Sprite mutedSprite;

    private bool masterMuted;
    private bool musicMuted;
    private bool sfxMuted;
    private bool voiceMuted;

    private float lastMasterVolume = 1f;
    private float lastMusicVolume = 1f;
    private float lastSFXVolume = 1f;
    private float lastVoiceVolume = 1f;

    [Header("Font")]
    [SerializeField] Slider fontSlider;
    [SerializeField] TMP_Text fontText;
    [SerializeField] TMP_Text[] allUIText;

    float defaultOrthoSize = 5f;

    private void Start()
    {
        LoadVolume();
    }

    // =====================
    // MENU
    // =====================

    public void ToggleSettings()
    {
        advSettingsMenu.SetActive(true);
        mainSettingsMenu.SetActive(false);
    }

    public void Exit()
    {
        advSettingsMenu.SetActive(false);
        mainSettingsMenu.SetActive(true);
    }

    public void LeaveApp()
    {
        Application.Quit();
    }

    // =====================
    // CAMERA ZOOM
    // =====================

    public void ZoomIn()
    {
        mainCamera.orthographicSize -= 1f;
    }

    public void ZoomOut()
    {
        mainCamera.orthographicSize += 1f;
    }

    // =====================
    // AUDIO
    // =====================

    void SetVolume(string parameter, float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }

    public void SetMasterVolume()
    {
        SetVolume("Master", masterSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        SetVolume("Music", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SetSoundVolume()
    {
        SetVolume("SFX", soundSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", soundSlider.value);
    }
    public void SetVoiceVolume()
    {
        SetVolume("Voice", voiceSlider.value);
        PlayerPrefs.SetFloat("voiceVolume", voiceSlider.value);
    }

    // =====================
    // MUTE TOGGLES
    // =====================

    public void ToggleMuteMaster()
    {
        if (!masterMuted)
        {
            lastMasterVolume = masterSlider.value;
            mixer.SetFloat("Master", -80f);
            masterIcon.sprite = mutedSprite;
        }
        else
        {
            //masterSlider.value = lastMasterVolume;
            SetMasterVolume();
            masterIcon.sprite = volumeSprite;
        }

        masterMuted = !masterMuted;
    }

    public void ToggleMuteMusic()
    {
        if (!musicMuted)
        {
            lastMusicVolume = musicSlider.value;
            mixer.SetFloat("Music", -80f);
            musicIcon.sprite = mutedSprite;
        }
        else
        {
            //musicSlider.value = lastMusicVolume;
            SetMusicVolume();
            musicIcon.sprite = volumeSprite;
        }

        musicMuted = !musicMuted;
    }

    public void ToggleMuteSound()
    {
        if (!sfxMuted)
        {
            lastSFXVolume = soundSlider.value;
            mixer.SetFloat("SFX", -80f);
            sfxIcon.sprite = mutedSprite;
        }
        else
        {
            //soundSlider.value = lastSFXVolume;
            SetSoundVolume();
            sfxIcon.sprite = volumeSprite;
        }

        sfxMuted = !sfxMuted;
    }
    public void ToggleMuteVoice()
    {
        if (!voiceMuted)
        {
            lastVoiceVolume = voiceSlider.value;
            mixer.SetFloat("Voice", -80f);
            voiceIcon.sprite = mutedSprite;
        }
        else
        {
            //voiceSlider.value = lastVoiceVolume;
            SetVoiceVolume();
            voiceIcon.sprite = volumeSprite;
        }

        voiceMuted = !voiceMuted;
    }

    void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        soundSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        voiceSlider.value = PlayerPrefs.GetFloat("voiceVolume", 1f);

        SetMasterVolume();
        SetMusicVolume();
        SetSoundVolume();
        SetVoiceVolume();
    }

    // =====================
    // FONT SIZE
    // =====================

    public void SetFontSize()
    {
        float size = fontSlider.value;

        foreach (TMP_Text text in allUIText)
        {
            text.fontSize = size;
        }

        fontText.text = "A";
    }
}