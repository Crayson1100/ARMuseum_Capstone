using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoDisplay : MonoBehaviour
{
    public VideoPlayer player;
    [Space(5)]
    public List<ArtData> art;
    private int currentVideo = 0;
    [SerializeField] GameObject videoPanel;

    [Header("Mute Button Images")]
    [SerializeField] Image playButtonImage;
    [SerializeField] Image muteButtonImage;

    [Header("Volume Sprites")]
    [SerializeField] Sprite pauseButtonSprite;
    [SerializeField] Sprite mutedSprite;
    [SerializeField] Sprite unmutedSprite;

    private Sprite lastPlaySprite;
    private Sprite lastMuteSprite;

    private void Start()
    {
        if (art[currentVideo] != null && art[currentVideo].type == ArtData.Type.MOVIE)
        {
            player.clip = art[0].clip;
            SizeToParent(player, this.gameObject.transform);
            player.SetDirectAudioMute(0, true);

        }
    }
    public Vector2 SizeToParent(VideoPlayer _player, Transform parent, float padding = 0)
    {
        if (parent == null || player.texture == null) return player.transform.localScale;

        padding = 1 - padding;

        float videoRatio = player.texture.width / player.texture.height;

        //Vector3 parentScale = parent.localScale;
        float parentWidth = parent.localScale.x;
        float parentHeight = parent.localScale.y;


        float w = parentHeight * padding * videoRatio;
        float h = parentHeight * padding;

        //h = parentScale.y * padding;
        //w = h * videoRatio;

        if (w > parentWidth * padding)
        {
            w = parentWidth * padding;
            h = w / videoRatio;
        }
        Vector3 newScale = new Vector3(w, h, player.transform.localScale.z);
        player.transform.localScale = newScale;

        return newScale;
    }
    [ContextMenu("Next Video")]
    public void NextVideo()
    {
        if (currentVideo == art.Count - 1)
        {
            currentVideo = 0;
        }
        else { currentVideo++; }
    }
    [ContextMenu("Previous Video")]
    public void PreviousVideo()
    {
        if (currentVideo == 0)
        {
            currentVideo = art.Count - 1;
        }
        else
        {
            currentVideo--;
        }
    }
    public void ShowPanel()
    {
        videoPanel.SetActive(true);
    }
    public void ExitPanel()
    {
        videoPanel.SetActive(false);
    }
}
