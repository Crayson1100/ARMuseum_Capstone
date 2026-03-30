using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CameraSpawn : MonoBehaviour
{
    [Header("AR Components")]
    public ARTrackedImageManager trackedImageManager;

    [Header("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    [Header("Object To Move (Usually AR Session Origin)")]
    public Transform arContentRoot;

    [Header("Museum Root (Disabled at Start)")]
    public GameObject museumRoot;

    private bool hasSpawned = false;

    private void Start()
    {
        // Ensure museum is hidden at the beginning
        if (museumRoot != null)
            museumRoot.SetActive(false);
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        if (hasSpawned)
            return;

        foreach (var trackedImage in args.added)
            HandleImage(trackedImage);

        foreach (var trackedImage in args.updated)
            HandleImage(trackedImage);
    }

    private void HandleImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState != TrackingState.Tracking)
            return;

        string imageName = trackedImage.referenceImage.name;

        switch (imageName)
        {
            case "Blocs Logo":
                SpawnAt(spawnPoint1);
                break;

            case "sad ethan":
                SpawnAt(spawnPoint2);
                break;

            case "Dragon":
                SpawnAt(spawnPoint3);
                break;

            default:
                Debug.LogWarning("Unrecognized image: " + imageName);
                return;
        }

        DisableTracking();
    }

    private void SpawnAt(Transform spawn)
    {
        if (spawn == null || arContentRoot == null)
            return;

        // Move AR content to the spawn point
        arContentRoot.position = spawn.position;
        arContentRoot.rotation = spawn.rotation;

        // Activate the museum
        if (museumRoot != null)
            museumRoot.SetActive(true);

        hasSpawned = true;

        Debug.Log("Museum activated and AR content moved to: " + spawn.name);
    }

    private void DisableTracking()
    {
        // Disable tracking system
        trackedImageManager.enabled = false;

        // Hide any tracked image objects still visible
        foreach (var trackedImage in trackedImageManager.trackables)
            trackedImage.gameObject.SetActive(false);

        Debug.Log("Image tracking disabled after spawn.");
    }
}