using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();


    private void Start()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();


        trackedImages.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    private void OnTrackablesChanged(
        ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var entry in args.added)
        {
            //Trackable Image loaded into image tracker

            SpawnPrefab(entry);
        }

        foreach (var entry in args.updated)
        {
            //Updating / Image on Screen

        }

        foreach (var entry in args.removed)
        {
            // image removed from tracking
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
        GameObject prefab = FindPrefab(trackedImage.referenceImage.name);

        if (prefab == null) return;

        if (spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name)) return;

        ARAnchor anchor = trackedImage.gameObject.AddComponent<ARAnchor>();

        GameObject obj = Instantiate(prefab, trackedImage.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        spawnedPrefabs.Add(trackedImage.referenceImage.name, obj);
    }


    
}