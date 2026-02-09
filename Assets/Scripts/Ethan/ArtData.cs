using System;
using UnityEngine;

/*
 * This will hold our exhibit data
 * 
 */
[CreateAssetMenu(fileName = "ArtData", menuName = "Scriptable Objects/ArtData")]
[Serializable]
public class ArtData : ScriptableObject
{
    public enum Type 
    {
        Model,
        Animation,
        Movie,
        Art
    }
    public int GalleryReference;


    public Type type;
    public string ArtName;
    public string ArtDescription;
    public string ArtistName;



}
