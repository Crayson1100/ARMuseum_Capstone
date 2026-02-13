using System;
using UnityEngine;

[Serializable]
public class Exhibit : MonoBehaviour
{

    public ArtData art;
    private GameObject displayObject;

    public Transform[] artLocation;


    private void Start()
    {
        displayObject = Instantiate<GameObject>(art.model, artLocation[0].position, Quaternion.identity);

        ScaleModel(displayObject.transform,this.transform);
       

    }

/// <summary>
/// Scale a model to an exhibits size
/// </summary>
/// <param name="model"></param>
/// <param name="exhibit"></param>
    public static void ScaleModel(Transform model, Transform exhibit)
    {
        Renderer[] modelRenderers = model.GetComponentsInChildren<Renderer>();
        Renderer[] tableRenderers = exhibit.GetComponentsInChildren<Renderer>(); 

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
