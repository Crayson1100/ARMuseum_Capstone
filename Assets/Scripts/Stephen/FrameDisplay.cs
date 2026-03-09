using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class FrameDisplay : MonoBehaviour
{

    public ArtData art;
    private List<GameObject> displayObject = new();

    public Transform[] artLocation;


    private void Start()
    {
        if (art != null)
        {
            if (artLocation.Length > 1)
            {
                for (int i = 0; i < artLocation.Length; i++)
                {
                    //displayObject.Add(Instantiate<Image>(art.model, artLocation[i].position, Quaternion.identity));

                    ScaleModels(displayObject[i].transform, this.transform, artLocation.Count());
                }
            }
        }

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

        Vector3 objectSize = GetBounds(modelRenderers).size;
        Vector3 tableSize = GetBounds(tableRenderers).size;

        float scaleX = tableSize.x / objectSize.x;
        float scaleZ = tableSize.z / objectSize.z;

        float scaleFactor = Mathf.Min(scaleX, scaleZ);

        model.localScale *= scaleFactor;
    }
    public static void ScaleModels(Transform model, Transform exhibit, int displayLocations)
    {
        Renderer[] modelRenderers = model.GetComponentsInChildren<Renderer>();
        Renderer[] tableRenderers = exhibit.GetComponentsInChildren<Renderer>();

        if (tableRenderers.Length == 0 || tableRenderers == null) return;
        if (modelRenderers.Length == 0 || modelRenderers == null) return;

        model.localScale = Vector3.one;

        Vector3 objectSize = GetBounds(modelRenderers).size;
        Vector3 tableSize = GetBounds(tableRenderers).size;

        float scaleX = tableSize.x / objectSize.x;
        float scaleZ = tableSize.z / objectSize.z;

        float scaleFactor = Mathf.Min(scaleX, scaleZ);

        model.localScale *= (scaleFactor / displayLocations);
    }
    private static Bounds GetBounds(Renderer[] renderers)
    {
        Bounds returnBounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            returnBounds.Encapsulate(r.bounds);
        }
        return returnBounds;
    }

    private void OnDrawGizmos()
    {
        foreach (var v in artLocation)
        {
            Gizmos.color = Color.blueViolet;
            Gizmos.DrawSphere(v.position, 0.1f);
        }
    }
}
