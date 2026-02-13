//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using UnityEngine;

//public static class ArtDataRepository
//{
//    static string path = Application.persistentDataPath;


//    private const string Header = "Type, ArtName, ArtDescription, ArtistName";
    

//    public static void Save(string path, List<ArtData> data, Texture2D exhibitImage)
//    {
//        var sb = new StringBuilder();
//        sb.AppendLine(Header);

//        foreach (var art in data)
//        {
//            sb.AppendLine(
//                $"{art.type}," +
//                $"{Escape(art.ArtName)}," +
//                $"{Escape(art.ArtDescription)}," +
//                $"{Escape(art.ArtistName)}"
//            );
//            File.WriteAllText(path, sb.ToString());
//        }
//    }
//    public static List<ArtData> Load(string csvPath, string basePath)
//    {
//        var returnList = new List<ArtData>();

//        if (!File.Exists(csvPath)) return returnList;

//        var lines = File.ReadAllLines(csvPath);

//        for (int i = 1; i < lines.Length; i++)
//        {
//            var columns = lines[i].Split(',');
//            if (columns.Length < 5) continue; //invalid row

//            var art = new ArtData
//            {
//                type = (ArtData.Type)System.Enum.Parse(typeof(ArtData.Type), columns[0]), //parse the text type into enum
//                ArtName = columns[1],
//                ArtDescription = columns[2],
//                ArtistName = columns[3],
//                RelativePath = columns[4],
//            };
            
//            art.FilePath = Path.Combine(basePath, art.RelativePath);

//            returnList.Add(art);
//        }
//            return returnList;
//    }
//    public static Texture2D LoadGalleryTexture (string exhibitFolderPath, string textureFileName = "ExibitTexture.png")
//    {
//        string texturePath = Path.Combine(exhibitFolderPath, textureFileName);
//        if (!File.Exists(texturePath)) return null;

//        byte[] bytes = File.ReadAllBytes(texturePath);
//        Texture2D texture = new Texture2D(2, 2);
//        texture.LoadImage(bytes);
//        return texture;
//    }
//    private static string Escape(string value)
//    {
//        if (string.IsNullOrEmpty(value))
//            return "";

//        return $"\"{value.Replace("\"", "\"\"")}\"";
//    }
//}
