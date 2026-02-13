using System;
using UnityEngine;

[Serializable]
public class Exhibit : MonoBehaviour
{
    /*
     * 
     * This load art/models from the exhibit lists
     * This will validate art/model scaling
     * Needs methods to output text/audio for art that have them
     * 
     * 
     */

    public ArtData art;
    public Transform localPosition;
    private GameObject displayObject;

    public Transform[] artLocation;


    private void Start()
    {
        displayObject = Instantiate<GameObject>(art.model, artLocation[0].position, Quaternion.identity);
        ScaleModel(displayObject.transform);
       

    }

    /// <summary>
    /// Scales a given models renderers to the same size as our exhibit renderers
    /// </summary>
    /// <param name="model"> the transform of the model</param>
    private void ScaleModel(Transform model)
    {
        Renderer[] modelRenderers = model.GetComponentsInChildren<Renderer>();
        Renderer[] tableRenderers = GetComponentsInChildren<Renderer>();

        if (tableRenderers.Length == 0 || tableRenderers == null) return;
        if (modelRenderers.Length == 0 || modelRenderers == null) return;

        model.localScale = Vector3.one;

        Bounds modelBounds = modelRenderers[0].bounds;
        foreach (Renderer r in modelRenderers)
        {
            modelBounds.Encapsulate(r.bounds);
        }


        Bounds tableBounds = tableRenderers[0].bounds;
        foreach (Renderer r in tableRenderers)
        {
            tableBounds.Encapsulate(r.bounds);
        }

        Vector3 objectSize = modelBounds.size;
        Vector3 tableSize = tableBounds.size;

        float scaleX = tableSize.x / objectSize.x;
        float scaleZ = tableSize.z / objectSize.z;

        float scaleFactor = Mathf.Min(scaleX, scaleZ);

        model.localScale *= scaleFactor;
    }
}
