using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public enum Type
    {
        END, TILES, CHILDREN
    }

    //public static readonly string saveFile = @"C:\UnityProjects\TiledGame\save.txt";
    public static readonly string saveFile = Application.persistentDataPath +Path.DirectorySeparatorChar + "saveData.txt";
    public static List<string> tiles = new List<string>();

    public static void loadSave()
    {
        Debug.Log(saveFile);
        SaveData.tiles.Clear();
        string[] text = System.IO.File.ReadAllLines(saveFile);

        //List<SaveData> lineData = new List<SaveData>();
        SaveData.Type type = SaveData.Type.END;

        foreach (string lines in text)
        {
            if (lines.StartsWith(SaveData.Type.END.ToString()))
            {
                type = SaveData.Type.END;
            }

            if (type == SaveData.Type.TILES)
            {
                SaveData.tiles.Add(lines);
            }

            if (lines.StartsWith(SaveData.Type.TILES.ToString()))
            {
                type = SaveData.Type.TILES;
            }
        }

        
    }


    public static Tile[,] loadTiles(Tile tileRef)
    {
        Tile[,] tiles = null;
        string[] tilesX = new string[SaveData.tiles.Count];

        int xSize = SaveData.tiles.Count;
        int zSize = -1;

        for (int x = 0; x < SaveData.tiles.Count; x++)
        {
            string[] xTiles = SaveData.tiles[x].Split(',');

            if (zSize == -1)
            {
                zSize = xTiles.Length;
                tiles = new Tile[xSize, zSize];
            }

            for (int z = 0; z < xTiles.Length; z++)
            {
                string id = xTiles[z];
                Tile t = Instantiate(tileRef, new Vector3(x, 0, z), Quaternion.identity);
                tiles[x, z] = t;
                t.setTile(x, z);

                t.setTileInfo(getTileInfo(id));
            }
        }

        return tiles;
    } 

    private static TileInfo getTileInfo(string id)
    {
        //if (id == "0")
        //{

        //}
        //else if (id == "1")
        //{
        //    return new TileInfo_Anime();
        //}
        //else if (id == "2")
        //{
        //    return new TileInfo_RED();
        //}
        Debug.Log(id);
        if(id.Length < 1)
        {
            return Ref.Instance.tileInfo[0];
        }
        return Ref.Instance.tileInfo[int.Parse(id)];
    }

}
