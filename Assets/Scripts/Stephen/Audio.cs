using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip background;
    public static AudioSettings instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keep this object alive
        }
        else
        {
            Destroy(gameObject); // destroy duplicate instances
        }
    }

    private void Start()
    {
        if (!musicSource.isPlaying) // optional safety check
        {
            musicSource.clip = background;
            musicSource.Play();
        }
    }
}
