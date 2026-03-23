using System.Collections.Generic;
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

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voiceSlider;

    [Header("Voice Sources")]
    [SerializeField] List<AudioSource> voiceSources = new List<AudioSource>();

    [Header("Mute Button Images")]
    [SerializeField] Image masterButtonImage;
    [SerializeField] Image musicButtonImage;
    [SerializeField] Image sfxButtonImage;
    [SerializeField] Image voiceButtonImage;

    [Header("Volume Sprites")]
    [SerializeField] Sprite maxVolumeSprite;
    [SerializeField] Sprite halfVolumeSprite;
    [SerializeField] Sprite quarterVolumeSprite;
    [SerializeField] Sprite mutedSprite;

    private Sprite lastMasterSprite;
    private Sprite lastMusicSprite;
    private Sprite lastSFXSprite;
    private Sprite lastVoiceSprite;



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

    private void Start()
    {
        LoadVolume();
        RefreshAllIcons();
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

    public void ZoomIn() => mainCamera.orthographicSize -= 1f;
    public void ZoomOut() => mainCamera.orthographicSize += 1f;

    // =====================
    // AUDIO
    // =====================

    private void SetVolume(string parameter, float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }

    public void SetMasterVolume()
    {
        if (!masterMuted)
        {
            float value = masterSlider.value;
            SetVolume("Master", value);
            PlayerPrefs.SetFloat("masterVolume", value);

            lastMasterSprite = GetVolumeSprite(value);
            masterButtonImage.sprite = lastMasterSprite;
        }
    }
    public void SetMusicVolume()
    {
        if (!musicMuted)
        {
            float value = musicSlider.value;
            SetVolume("Music", value);
            PlayerPrefs.SetFloat("musicVolume", value);

            lastMusicSprite = GetVolumeSprite(value);
            musicButtonImage.sprite = lastMusicSprite;
        }
    }
    public void SetSFXVolume()
    {
        if (!sfxMuted)
        {
            float value = sfxSlider.value;
            SetVolume("SFX", value);
            PlayerPrefs.SetFloat("sfxVolume", value);

            lastSFXSprite = GetVolumeSprite(value);
            sfxButtonImage.sprite = lastSFXSprite;
        }
    }

    public void SetVoiceVolume()
    {
        if (!voiceMuted)
        {
            float value = voiceSlider.value;

            SetVolume("Voice", value);
            PlayerPrefs.SetFloat("voiceVolume", value);

            foreach (AudioSource src in voiceSources)
                if (src != null)
                    src.volume = value;

            lastVoiceSprite = GetVolumeSprite(value);
            voiceButtonImage.sprite = lastVoiceSprite;
        }
    }


    Sprite GetVolumeSprite(float value)
    {
        if (value <= 0.001f)
            return mutedSprite;

        if (value >= 0.75f)
            return maxVolumeSprite;

        if (value >= 0.40f)
            return halfVolumeSprite;

        return quarterVolumeSprite;
    }


    // =====================
    // MUTE TOGGLES
    // =====================

    public void ToggleMuteMaster()
    {
        masterMuted = !masterMuted;

        if (masterMuted)
        {
            lastMasterVolume = masterSlider.value;
            mixer.SetFloat("Master", -80f);
        }
        else
        {
            SetMasterVolume();
        }

        UpdateIcon(masterButtonImage, masterMuted, lastMasterSprite);
    }



    public void ToggleMuteMusic()
    {
        musicMuted = !musicMuted;

        if (musicMuted)
        {
            lastMusicVolume = musicSlider.value;
            mixer.SetFloat("Music", -80f);
        }
        else
        {
            SetMusicVolume();
        }

        UpdateIcon(musicButtonImage, musicMuted, lastMusicSprite);
    }


    public void ToggleMuteSFX()
    {
        sfxMuted = !sfxMuted;

        if (sfxMuted)
        {
            lastSFXVolume = sfxSlider.value;
            mixer.SetFloat("SFX", -80f);
        }
        else
        {
            SetSFXVolume();
        }

        UpdateIcon(sfxButtonImage, sfxMuted, lastSFXSprite);
    }


    public void ToggleMuteVoice()
    {
        if (!voiceMuted)
        {
            lastVoiceVolume = voiceSlider.value;
            mixer.SetFloat("Voice", -80f);

            foreach (AudioSource src in voiceSources)
                if (src != null)
                    src.mute = true;
        }
        else
        {
            SetVoiceVolume();

            foreach (AudioSource src in voiceSources)
                if (src != null)
                    src.mute = false;
        }

        voiceMuted = !voiceMuted;
        UpdateIcon(voiceButtonImage, voiceMuted, lastVoiceSprite);
    }
    void UpdateIcon(Image icon, bool isMuted, Sprite lastSprite)
    {
        icon.sprite = isMuted ? mutedSprite : lastSprite;
    }


    private void RefreshAllIcons()
    {
        UpdateIcon(masterButtonImage, masterMuted, lastMasterSprite);
        UpdateIcon(musicButtonImage, musicMuted, lastMusicSprite);
        UpdateIcon(sfxButtonImage, sfxMuted, lastSFXSprite);
        UpdateIcon(voiceButtonImage, voiceMuted, lastVoiceSprite);

    }

    // =====================
    // LOAD SETTINGS
    // =====================

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        voiceSlider.value = PlayerPrefs.GetFloat("voiceVolume", 1f);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetVoiceVolume();
    }

    // =====================
    // FONT SIZE
    // =====================

    public void SetFontSize()
    {
        float size = fontSlider.value;

        foreach (TMP_Text text in allUIText)
            text.fontSize = size;

        fontText.text = "A";
    }
}