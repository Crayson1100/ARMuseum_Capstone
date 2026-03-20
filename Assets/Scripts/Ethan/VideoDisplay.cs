using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
}
