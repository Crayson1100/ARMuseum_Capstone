using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject masterSlider, masterButton, musicSlider, musicButton, soundSlider, soundButton;//sound panel
    [SerializeField] TMP_Text fontText;
    [SerializeField] TMP_Text[] allUIText;
    [SerializeField] Slider fontSlider;
    [SerializeField] Camera mainCamera;
    //[SerializeField] AudioMixer mixer;
    [SerializeField] GameObject settingsMenu, exitButton;

    private void Awake()
    {
        settingsMenu.SetActive(false);
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }
    public void ZoomIn()
    {
        mainCamera.orthographicSize -= 1f;
    }

    public void ZoomOut()
    {
        mainCamera.orthographicSize += 1f;
    }
    public void MasterVolume()
    {
        //slider function controls level
        //button function will mute ALL audio
    }
    public void Music()
    {
        //slider function controls level
        //button function will mute only Music audio
    }
    public void Sound()
    {
        //slider function controls level
        //button function will mute only Sound audio
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
