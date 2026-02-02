using UnityEngine;
using static ExhibitEnvironment;

public class ARExhibitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject environmentPrefab;

    public void OnBarcodeScanned(string exhibitId)
    {
        Debug.Log($"Scanned exhibit: {exhibitId}");

        ExhibitData data = ExhibitLoader.LoadExhibit(exhibitId);
        if (data == null)
        {
            Debug.LogError("Failed to load exhibit data.");
            return;
        }

        // Spawn at camera forward position
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
        Quaternion spawnRot = Quaternion.LookRotation(Camera.main.transform.forward);

        GameObject env = Instantiate(environmentPrefab, spawnPos, spawnRot);

        var exhibitEnv = env.GetComponent<ExhibitEnvironment>();
        exhibitEnv.Initialize(data);
    }
}

