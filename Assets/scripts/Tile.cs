using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    int tileX;
    int tileZ;

    float r;
    float g;
    float b;

    //bool wasDragging = false;

    private TileInfo tileInfo;

    public List<PlaceableObject> placeableObjects;
    private PlaceableObject child;
    private Vector2 dragDistance;

    //Material mat;

    private void Start()
    {
        //mat = GetComponent<Renderer>().material;
    }

    public PlaceableObject getChild()
    {
        return child;
    }

    public bool hasChild() {
        return child != null;
    }

    private void OnMouseDown()
    {
        //Debug.Log("Clicked Color: [r " + r + " g " + g + " b " + b + "]");
        //mat.SetColor("_Color", new Color(1, 1, 1, 1));
        //Debug.Log("Clicked: " + tileX + " : " + tileZ);
        //bool ui = EventSystem.current.IsPointerOverGameObject();
        dragDistance = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

    }

    //temp code
    public void addChild()
    {
        int r = Random.Range(0, placeableObjects.Capacity);
        //setColor();
        //child = Instantiate(placeableObjects[r], new Vector3(tileX, 0, tileZ), Quaternion.identity);
    }

    private void OnMouseUp()
    {
        //float dist = Vector2.Distance(dragDistance, Input.mousePosition);
        //if(dist > 1)
        //{
            //wasDragging = true;
        //}
        bool ui = Utility.isOverUI();
        bool isBuilding = Tool.getSelected() == Tool.selection.BUILD;
        bool isRemoving = Tool.getSelected() == Tool.selection.REMOVE;

        //TODO fix dragging 
        if (!ui && isBuilding && !Utility.wasDragged(1) && !hasChild())
        {
            int r = Random.Range(0, placeableObjects.Capacity);
            //setColor();
            child = Instantiate(placeableObjects[r], new Vector3(tileX, 0, tileZ), Quaternion.identity);
            
        }
        else if (!ui && isRemoving && !Utility.wasDragged(1))
        {
            destroyChildren();
        }

        if (hasChild())
        {
            isChairCheck();
        }
        //wasDragging = false;
    }

    //checks the chairs and see if their is a table with it
    private void checkforTableAndChair() {
    }

    //this will check for any tables the chair is beside and rotate as needed
    private void isChairCheck()
    {
        if (getChild().type == PlaceableObject.TYPE.CHAIR)
        {
            //check facing direction and check for table
            bool hasHandled = false;
            int forward = tileZ + 1;
            int back = tileZ - 1;
            int left = tileX - 1;
            int right = tileX + 1;

            Debug.Log(forward + " Forward " + back + " Back " + left + " Left " + right + " Right");
            Debug.Log(child.type);

            if (!hasHandled && forward < GameData.tiles.GetLength(1) - 1)
            {
                if (GameData.tiles[tileX, forward].getChild() != null)
                {
                    //check forward;
                    if (GameData.tiles[tileX, forward].getChild().type == PlaceableObject.TYPE.TABLE)
                    {
                        Debug.Log("Forward Here! " + GameData.tiles[tileX, forward].getChild().type);
                        GameData.tiles[tileX, forward].tmpHightlight();
                        hasHandled = true;
                    }
                }
            }

            if (!hasHandled && left >= 0)
            {
                if (GameData.tiles[left, tileZ].getChild() != null)
                {
                    if (GameData.tiles[left, tileZ].getChild().type == PlaceableObject.TYPE.TABLE)
                    {
                        Debug.Log("LEFT Here!" + GameData.tiles[left, tileZ].getChild().type);
                        GameData.tiles[left, tileZ].tmpHightlight();
                        child.transform.rotation = Quaternion.Euler(Vector3.up * 270);
                        hasHandled = true;
                    }
                }
            }

            if (!hasHandled && back >= 0)
            {
                if (GameData.tiles[tileX, back].getChild() != null)
                {
                    if (GameData.tiles[tileX, back].getChild().type == PlaceableObject.TYPE.TABLE)
                    {
                        Debug.Log("BACK Here!" + GameData.tiles[tileX, back].getChild().type);
                        GameData.tiles[tileX, back].tmpHightlight();
                        child.transform.rotation = Quaternion.Euler(Vector3.up * 180);
                        hasHandled = true;
                    }
                }
            }

            if (!hasHandled && right < GameData.tiles.GetLength(0) - 1)
            {
                if (GameData.tiles[right, tileZ].getChild() != null)
                {
                    if (GameData.tiles[right, tileZ].getChild().type == PlaceableObject.TYPE.TABLE)
                    {
                        Debug.Log("RIGHT Here!" + GameData.tiles[right, tileZ].getChild().type);
                        GameData.tiles[right, tileZ].tmpHightlight();
                        child.transform.rotation = Quaternion.Euler(Vector3.up * 90);
                        hasHandled = true;
                    }
                }
            }
        }
    }

    public void tmpHightlight() {
        Renderer[] childRenders = child.GetComponentsInChildren<Renderer>();
        foreach(Renderer c in childRenders)
        {
            c.material.SetColor("_BaseColor", new Color(1, 1, 0, 0.8f));
        }
        //child.gameObject.GetComponentsInChildren<Renderer>().material.SetColor("_BaseColor", new Color(1, 1, 0, 0.8f));
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        if(EventSystem.current.IsPointerOverGameObject())
    //        {
    //            Debug.Log("Clicked on UI");
    //        }
    //        else
    //        {
    //            Debug.Log("Clicked: " + tileX + " : " + tileZ);
    //        }
    //    }
    //}

    public void destroyChildren() {
        if (child != null)
        {
            Destroy(child.gameObject);
        }
    }
    private void OnMouseOver()
    {
        //bool ui = EventSystem.current.IsPointerOverGameObject();
        bool ui = Utility.isOverUI();
        bool isBuilding = Tool.getSelected() == Tool.selection.BUILD;
        bool isRemoving = Tool.getSelected() == Tool.selection.REMOVE;
        if (!ui && (isBuilding || isRemoving)) 
        {
            GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1, 1, 0, 0.8f));
        }
        else
        {
            resetColor();
        }
    }

    private void OnMouseExit()
    {
        resetColor();
    }

    private void resetColor()
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1, 1, 1, 1));
    }

    public void setColor()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //Color color = new Color(Random.value, Random.value, Random.value, 1);
        //r = color.r;
        //g = color.g;
        //b = color.b;
        //renderer.material.SetColor("_Color", color);

        //Material newMat = Resources.Load("RED", typeof(Material)) as Material;
        //Material newMat = new Material(Shader.Find("RED"));
        if (Random.Range(0, 100) < 50)
        {
            setMaterial(MaterialMap.Instance.RED);
        }
        else
        {
            setMaterial(MaterialMap.Instance.TILE_MAT);
        }
        //GetComponent<Renderer>().material = newMat;
    }

    public void setTileInfo(TileInfo info)
    {
        tileInfo = info;
        refreshTileInfo();
    }

    public TileInfo getTileInfo() {
        return tileInfo;
    }

    private void refreshTileInfo() {
        setMaterial(tileInfo.getMaterial());
    }

    private void setMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

    public void setTile(int x, int z)
    {
        tileX = x;
        tileZ = z;
    }
}
