using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker1 : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;

    [Header("Assign any number of prefabs")]
    public List<GameObject> ArPrefabs = new List<GameObject>();

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();


    private void Start()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();

        trackedImages.trackablesChanged.AddListener(OnTrackablesChanged);
    }


    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var entry in args.added)
        {
            // Trackable Image loaded into image tracker
            SpawnPrefab(entry);
        }

        foreach (var entry in args.updated)
        {
            // Updating / Image on Screen
            UpdatePrefab(entry);
        }

        foreach (var entry in args.removed)
        {
            // Image removed from tracking
            RemovePrefab(entry.Value);
        }
    }


    private GameObject FindPrefab(string name)
    {
        if (ArPrefabs.Any(x => x.name == name))
        {
            return ArPrefabs.First(x => x.name == name);
        }

        return null;
    }


    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        string key = trackedImage.referenceImage.name;

        if (spawnedPrefabs.ContainsKey(key)) return;

        GameObject prefab = FindPrefab(key);
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, trackedImage.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        spawnedPrefabs.Add(key, obj);
    }


    private void UpdatePrefab(ARTrackedImage trackedImage)
    {
        string key = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(key, out GameObject obj))
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                obj.SetActive(true);
                obj.transform.position = trackedImage.transform.position;
                obj.transform.rotation = trackedImage.transform.rotation;
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }


    private void RemovePrefab(ARTrackedImage trackedImage)
    {
        string key = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(key, out GameObject obj))
        {
            obj.SetActive(false);
        }
    }


    // ---------------------------------------------------------
    // UI METHODS
    // ---------------------------------------------------------

    // Called by UI buttons to show a prefab by index
    public void ShowPrefabByIndex(int index)
    {
        if (index < 0 || index >= ArPrefabs.Count)
        {
            Debug.LogWarning("UI requested prefab index out of range");
            return;
        }

        GameObject prefab = ArPrefabs[index];

        // If already spawned, just enable it
        if (spawnedPrefabs.TryGetValue(prefab.name, out GameObject existing))
        {
            existing.SetActive(true);
            return;
        }

        // Spawn at world origin (or wherever you want UI items to appear)
        GameObject obj = Instantiate(prefab);
        obj.name = prefab.name;

        spawnedPrefabs.Add(prefab.name, obj);
    }


    // Hide all spawned prefabs (optional UI button)
    public void HideAllPrefabs()
    {
        foreach (var entry in spawnedPrefabs)
        {
            entry.Value.SetActive(false);
        }
    }
}