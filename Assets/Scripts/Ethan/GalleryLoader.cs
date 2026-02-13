//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;

//public class GalleryLoader : MonoBehaviour
//{
//    private ARTrackedImageManager imageManager;
//    private Dictionary<Texture2D, string> galleryCollection = new Dictionary<Texture2D, string>();
//    public List<string> loadedGalleries = new List<string>();
//    private void Awake()
//    {
//        LoadAllGalleries();
//        foreach (var gallery in galleryCollection.Values)
//        {
//            loadedGalleries.Add(gallery);
//        }

//    }

//    private void LoadAllGalleries()
//    {
//        string galleryRoot = Path.Combine(Application.persistentDataPath, "Galleries");

//        if (!Directory.Exists(galleryRoot))
//        {
//            Debug.LogWarning("No gallery directory found: creating new directory");
//            Directory.CreateDirectory(galleryRoot);
//        }

//        //var count = Directory.GetDirectories(galleryRoot).Length;
//        //Debug.Log(count);

//        foreach (string folder in Directory.GetDirectories(galleryRoot))
//        {
//            Texture2D trackedTexture = ArtDataRepository.LoadGalleryTexture(folder);
//            if ( trackedTexture != null)
//            {
//                galleryCollection[trackedTexture] = folder;
                
//            }
//            else
//            {
//                Debug.LogWarning("No texture found for the exhibit folder: " + folder);
//            }
//        }
//    }

//    private void AddTextureToARLibrary()
//    {

//    }
//}
