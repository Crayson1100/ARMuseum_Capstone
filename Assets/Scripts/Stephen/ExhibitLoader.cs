using System.IO;
using UnityEngine;
using static ExhibitEnvironment;

public static class ExhibitLoader
{
    public static ExhibitData LoadExhibit(string exhibitId)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Exhibits", exhibitId + ".json");

        if (!File.Exists(path))
        {
            Debug.LogError($"Exhibit JSON not found at: {path}");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<ExhibitData>(json);
    }
}

