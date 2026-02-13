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

    public Transform[] artLocation;


    private void Start()
    {
        var obj = Instantiate<GameObject>(art.model, artLocation[0].position, Quaternion.identity);


    }
}
