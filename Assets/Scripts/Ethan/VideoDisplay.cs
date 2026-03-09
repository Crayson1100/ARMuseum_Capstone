using UnityEngine;
using UnityEngine.Video;

public class VideoDisplay : MonoBehaviour
{
    public VideoPlayer player;
    [Space(5)]
    public ArtData art;


    private void Start()
    {
        if (art != null && art.type == ArtData.Type.MOVIE)
        {
            player.clip = art.clip;
            player.SetDirectAudioMute(0, true);
        }
    }
}
