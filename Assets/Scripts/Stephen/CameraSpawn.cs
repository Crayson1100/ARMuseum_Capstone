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

    private bool hasSpawned = false;

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
                MoveToSpawn(spawnPoint1);
                break;

            case "sad ethan":
                MoveToSpawn(spawnPoint2);
                break;

            case "Dragon":
                MoveToSpawn(spawnPoint3);
                break;

            default:
                Debug.LogWarning("Unrecognized image: " + imageName);
                break;
        }

        // Disable all tracked images after first success
        DisableAllTracking();
    }

    private void MoveToSpawn(Transform spawn)
    {
        if (spawn == null || arContentRoot == null)
            return;

        arContentRoot.position = spawn.position;
        arContentRoot.rotation = spawn.rotation;

        Debug.Log("Moved AR content to: " + spawn.name);
    }

    private void DisableAllTracking()
    {
        hasSpawned = true;

        // Disable the image tracking system
        trackedImageManager.enabled = false;

        // Hide any tracked image objects still in the scene
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            trackedImage.gameObject.SetActive(false);
        }

        Debug.Log("Image tracking disabled after first spawn.");
    }
}