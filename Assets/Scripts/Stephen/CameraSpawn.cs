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
            case "Image1":
                MoveToSpawn(spawnPoint1);
                break;

            case "Image2":
                MoveToSpawn(spawnPoint2);
                break;

            case "Image3":
                MoveToSpawn(spawnPoint3);
                break;

            default:
                Debug.LogWarning("Unrecognized image: " + imageName);
                break;
        }
    }

    private void MoveToSpawn(Transform spawn)
    {
        if (spawn == null || arContentRoot == null)
            return;

        arContentRoot.position = spawn.position;
        arContentRoot.rotation = spawn.rotation;

        Debug.Log("Moved AR content to: " + spawn.name);
    }
}
