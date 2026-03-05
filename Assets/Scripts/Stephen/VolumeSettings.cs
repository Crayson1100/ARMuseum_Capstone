using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Image masterIcon;
    [SerializeField] private Sprite volumeSprite;
    [SerializeField] private Sprite mutedSprite;

    private bool masterMuted;
    private bool musicMuted;
    private bool sfxMuted;

    private float lastMasterVolume = 1f;
    private float lastMusicVolume = 1f;
    private float lastSFXVolume = 1f;

    private void Start()
    {
        LoadVolume();
    }

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
        SetVolume("music", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        SetVolume("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

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
            masterSlider.value = lastMasterVolume;
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
            mixer.SetFloat("music", -80f);

            masterIcon.sprite = mutedSprite;
        }
        else
        {
            musicSlider.value = lastMusicVolume;
            SetMusicVolume();

            masterIcon.sprite = volumeSprite;
        }

        musicMuted = !musicMuted;
    }

    public void ToggleMuteSFX()
    {
        if (!sfxMuted)
        {
            lastSFXVolume = sfxSlider.value;
            mixer.SetFloat("SFX", -80f);

            masterIcon.sprite = mutedSprite;

        }
        else
        {
            sfxSlider.value = lastSFXVolume;
            SetSFXVolume();

            masterIcon.sprite = volumeSprite;

        }

        sfxMuted = !sfxMuted;
    }

    void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}