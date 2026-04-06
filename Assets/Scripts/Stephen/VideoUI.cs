using UnityEngine;

public class VideoUI : MonoBehaviour
{
    [Header("Videos to cycle through")]
    public GameObject[] videos;

    private int currentIndex = 0;

    void Start()
    {
        ShowVideoObject(currentIndex);
    }

    public void Next()
    {
        currentIndex++;

        if (currentIndex >= videos.Length)
            currentIndex = 0;

        ShowVideoObject(currentIndex);
    }

    public void Previous()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = videos.Length - 1;

        ShowVideoObject(currentIndex);
    }

    void ShowVideoObject(int index)
    {
        for (int i = 0; i < videos.Length; i++)
        {
            videos[i].SetActive(i == index);
        }
    }
}
