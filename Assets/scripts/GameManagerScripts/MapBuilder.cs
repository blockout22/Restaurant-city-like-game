using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBuilder : MonoBehaviour
{
    [Header("Reference to prefab tile")]
    public Tile tile;
    [Header("Reference to prefab wall")]
    public Wall wall;
    

    [Tooltip("The size of the map")]
    public int size = 2;
    private int lastSize;
    private bool isBuilding = true;

    private Tile[,] tiles;
    private Wall[,] walls;


    //private int totalTiles = 0;
    int x = 0;

    void Start()
    {
        lastSize = size;
        build();
    }

    private void build()
    {
        x = 0;
        clearTiles();
        clearWalls();
        tiles = new Tile[size, size];
        walls = new Wall[2, size];
        isBuilding = true;
    }

    private void buildNextLine()
    {
        if (x < tiles.GetLength(0))
        {

            for (int z = 0; z < tiles.GetLength(1); z++)
            {
                Tile t = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity) as Tile;
                tiles[x, z] = t;
                t.setTile(x, z);
                if (Random.value > 0.1)
                {
                    //t.setTileInfo(new TileInfo_RED());
                }
                else
                {
                    //t.setTileInfo(new TileInfo_Anime());
                }

                if (Random.value > 0.5f)
                {
                    t.addChild();
                }

                //front wall
                if (x == 0)
                {
                    wall = Instantiate(wall, new Vector3(x - (0.9f / 2), 0, z), Quaternion.identity);
                    walls[0, z] = wall;
                }

                //side wall
                if (z == tiles.GetLength(1) - 1)
                {
                    Quaternion quaternion = Quaternion.identity;
                    quaternion *= Quaternion.Euler(0, 90, 0);
                    wall = Instantiate(wall, new Vector3(x, 0, z + (0.9f / 2)), quaternion);
                    walls[1, x] = wall;
                }
                //t.setColor();
            }
            x++;
            //buildNextLine();
        }
        else
        {
            isBuilding = false;
        }
    }

    //used when recreating map to prevent creating duplicate objects by destroying existing objects
    private void clearTiles()
    {
        if(tiles == null)
        {
            Debug.Log("Tiles is null");
            return;
        }
        for (int x = tiles.GetLength(0) - 1; x >= 0; x--)
        {
            for (int z = tiles.GetLength(1) - 1; z >= 0; z--)
            {
                tiles[x, z].destroyChildren();
                Destroy(tiles[x, z].gameObject);
                Destroy(tiles[x, z]);
            }
        }
    }
    //used when recreating map to prevent creating duplicate objects by destroying existing objects
    private void clearWalls() {
        if(walls == null)
        {
            Debug.Log("walls is null");
            return;
        }

        for (int x = walls.GetLength(0) - 1; x >= 0; x--)
        {
            for (int z = walls.GetLength(1) - 1; z >= 0; z--)
            {
                Destroy(walls[x, z].gameObject);
                Destroy(walls[x, z]);
            }
        }
    }

    

    
    public void save()
    {
        
        //string[] sTiles = new string[tiles.GetLength(0)];
        System.IO.StreamWriter file = new System.IO.StreamWriter(SaveData.saveFile);

        //save tiles;
        file.WriteLine(SaveData.Type.TILES);
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            string line = "";
            for (int z = 0; z < tiles.GetLength(1); z++)
            {
                if (z > 0)
                {
                    line = line + ",";
                }
                //TODO get floor tile decoration and save it as a different number
                for(int i = 0; i < Ref.Instance.tileInfo.Length; i++)
                {
                    if(tiles[x, z].getTileInfo() == Ref.Instance.tileInfo[i])
                    {
                        Debug.Log(i);
                        line = line + i;
                    }
                }
                //if(tiles[x, z].getTileInfo().GetType() == typeof(TileInfo_Anime))
                //{
                    //line = line + "1";
               // }else if(tiles[x, z].getTileInfo().GetType() == typeof(TileInfo_RED))
                //{
                    //line = line + "2";
                //}
                
            }
            //sTiles[x] = line;
            file.WriteLine(line);
        }
        file.WriteLine(SaveData.Type.END);

        file.Flush();
        file.Close();
        file.Dispose();
        Ref.Instance.showText("Successfully Saved", 3);
        //System.IO.File.WriteAllLines(saveFile, sTiles);

    }

    public void load() {
        clearTiles();
        //clearWalls();

        SaveData.loadSave();
        tiles = SaveData.loadTiles(tile);
        Ref.Instance.showText("Successfully Loaded", 3);


        //int xSize = text.Length;
        //int ySize = -1;
        //for (int x = 0; x < text.Length; x++)
        //{
        //    string[] tileIDs = text[x].Trim().Split(',');
        //    if(ySize == -1)
        //    {
        //        ySize = tileIDs.Length;
        //        tiles = new Tile[xSize, ySize];
        //    }

        //        Debug.Log(x + " : " + tileIDs);
        //    for (int z = 0; z < tileIDs.Length; z++)
        //    {
        //        string id = tileIDs[z];
        //        Tile t = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity) as Tile;
        //        tiles[x, z] = t;
        //        t.setTile(x, z);
        //        if (id == "0")
        //        {

        //        }
        //        else if (id == "1")
        //        {
        //            t.setTileInfo(new TileInfo_Anime());
        //        }
        //        else if (id == "2")
        //        {
        //            t.setTileInfo(new TileInfo_RED());
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    bool wantsToBuild = false;
    void Update()
    {
        if(wantsToBuild)
        {
            build();
            wantsToBuild = false;
        }
        if(lastSize != size && size > 0 && !isBuilding)
        {
            build();
            lastSize = size;
        }

        if (isBuilding)
        {
            buildNextLine();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            save();
        }

            if (Input.GetKeyUp(KeyCode.N))
        {
            //Debug.Log("Why you destroy?");
            //wantsToBuild = true;
            //save();
            Invoke("load", 2);
        }
    }
}
