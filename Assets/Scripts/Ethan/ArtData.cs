
using System;
using System.Collections.Generic;


/*
 * This will hold our:
 * Artist Information
 * Arist Audio
 * Model/Art
 * 
 */

[Serializable]
public class ArtData 
{
    public enum Type {MODEL, ANIMATION, MOVIE, ART}
    public Type type;

    public string ArtName;
    public string ArtDescription;
    public string ArtistName;
    public string RelativePath;

    public string FilePath;

}
public class CollectionData
{
    public string ExhibitID;
    public string ExhibitName;
    public string ExhibitDescription;
    public List<ArtData> Artworks;

}
