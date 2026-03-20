
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
[CreateAssetMenu(fileName = "Art Data", menuName = "Art Data")]
public class ArtData : ScriptableObject
{
    public enum Type {MODEL, MOVIE, ART}
    public Type type;
    [Space(10)]
    public string ArtName;
    public string ArtDescription;
    [Space(2)]
    public string ArtistName;
    [Space(10)]
    public GameObject model;
    public VideoClip clip;
    public Texture image;
    public AudioClip audioClip;


}
public class CollectionData
{
    public string ExhibitID;
    public string ExhibitName;
    public string ExhibitDescription;
    public List<ArtData> Artworks;

}
