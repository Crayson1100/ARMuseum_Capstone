using UnityEngine;

public class ExhibitEnvironment : MonoBehaviour
{
    public ExhibitData Data { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMPro.TextMeshPro titleText;
    [SerializeField] private TMPro.TextMeshPro descriptionText;

    [Header("Spawn Parents")]
    [SerializeField] private Transform modelParent;
    [SerializeField] private Transform imageParent;

    public void Initialize(ExhibitData data)
    {
        Data = data;

        if (titleText != null)
            titleText.text = data.title;

        if (descriptionText != null)
            descriptionText.text = data.description;

        // Load all prefabs
        if (data.prefabPaths != null)
        {
            foreach (var path in data.prefabPaths)
            {
                var prefab = Resources.Load<GameObject>(path);
                if (prefab != null)
                {
                    Instantiate(prefab, modelParent != null ? modelParent : transform);
                }
                else
                {
                    Debug.LogWarning($"Prefab not found at: {path}");
                }
            }
        }

        // Load all images
        if (data.imagePaths != null)
        {
            foreach (var path in data.imagePaths)
            {
                var tex = Resources.Load<Texture2D>(path);
                if (tex != null)
                {
                    // You can convert this to a Sprite for UI
                    Sprite sprite = Sprite.Create(
                        tex,
                        new Rect(0, 0, tex.width, tex.height),
                        new Vector2(0.5f, 0.5f)
                    );

                    // Add to your UI gallery, image panel, etc.
                    // (depends on your UI setup)
                }
                else
                {
                    Debug.LogWarning($"Image not found at: {path}");
                }
            }
        }

        // Optional audio guide
        if (!string.IsNullOrEmpty(data.audioGuidePath))
        {
            var clip = Resources.Load<AudioClip>(data.audioGuidePath);
            if (clip != null)
            {
                var audio = gameObject.AddComponent<AudioSource>();
                audio.clip = clip;
                audio.playOnAwake = false;
            }
            else
            {
                Debug.LogWarning($"Audio not found at: {data.audioGuidePath}");
            }
        }
    }
}