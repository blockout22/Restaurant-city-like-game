using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utility : MonoBehaviour
{
    private static float dragDist = 0;
    private Vector2 dragVector;
    private static bool isTakingScreenShot = false;
    private static Camera screenshotCam;

    //private bool isMouseDown = false;

    //checks if the Cursor is over a UI gameobject 
    public static bool isOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static void screenshot(Camera targetCam, int width, int height)
    {
        screenshotCam = targetCam;
        screenshotCam.targetTexture = new RenderTexture(width, height, 24);
        isTakingScreenShot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragDist = 0;
            //isMouseDown = true;
            dragVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //isMouseDown = false;
            dragDist = Vector2.Distance(dragVector, Input.mousePosition);
        }
    }

    private void LateUpdate()
    {
        if (isTakingScreenShot)
        {
            RenderTexture rt = screenshotCam.targetTexture;
            Texture2D screenShot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            screenshotCam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            screenshotCam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();

            int i = 0;
            string folder = "screenshots";
            string filename = folder + "/Screenshot-" + i +".png";

            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            while (System.IO.File.Exists(filename))
            {
                i++;
                filename = folder + "/Screenshot-" + i + ".png";
            }

            
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            isTakingScreenShot = false;
        }
    }

    public static Tile[,] convertTo2d(Tile[] tiles, int sizeX, int sizeZ)
    {
        Tile[,] t = new Tile[sizeX, sizeZ];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                t[i, j] = tiles[i * j];
            }
        }

        return t;
    }

    public static Tile[] convertTo1D(Tile[,] t)
    {
        Tile[] tiles = new Tile[t.GetLength(0) * t.GetLength(1)];
        for (int i = 0; i < t.GetLength(0); i++)
        {
            for (int j = 0; j < t.GetLength(1); j++)
            {
                tiles[i * j] = t[i, j];
            }
        }

        return tiles;
    }

    //private void OnMouseDown()
    //{
    //Debug.Log("Mouse Down");
    //dragVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //}

    //private void OnMouseUp()
    // {
    //    dragDist = Vector2.Distance(dragVector, Input.mousePosition);
    //     Debug.Log("Mouse Up : " + dragDist);
    // }

    //checks if the mouse was dragged more than the tolorence distance
    public static bool wasDragged(float tolorence)
    {
        Debug.Log(Utility.dragDist);
        return Utility.dragDist > tolorence ? true : false;
    }
}
