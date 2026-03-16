using UnityEngine;
using UnityEngine.UI;

public class HeadsUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject headPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;

    [Header("Play/Pause Button Sprites")]
    [SerializeField] private Image playPauseButtonImage;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;

    [Header("Mute Button Sprites")]
    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite unmutedSprite;
    [SerializeField] private Sprite mutedSprite;

    private bool isMuted = false;

    private void Awake()
    {
        headPanel.SetActive(false);

        playPauseButtonImage.sprite = pauseSprite;
        muteButtonImage.sprite = unmutedSprite;
    }

    public void DisplayUI()
    {
        headPanel.SetActive(true);
    }

    public void ExitPanel()
    {
        headPanel.SetActive(false);
    }

    public void ToggleAudio()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            playPauseButtonImage.sprite = playSprite;
        }
        else
        {
            musicSource.Play();
            playPauseButtonImage.sprite = pauseSprite;
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        musicSource.mute = isMuted;

        muteButtonImage.sprite = isMuted ? mutedSprite : unmutedSprite;
    }
}