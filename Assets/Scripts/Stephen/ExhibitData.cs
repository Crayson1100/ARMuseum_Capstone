using System;

[Serializable]
public class ExhibitData
{
    public string id;
    public string title;
    public string description;

    // Multiple prefabs to spawn in the AR environment
    public string[] prefabPaths;

    // Multiple images for UI, galleries, thumbnails, etc.
    public string[] imagePaths;

    // Optional metadata
    public string audioGuidePath;
}