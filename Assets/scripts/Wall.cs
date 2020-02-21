using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnMouseOver()
    {
        bool ui = Utility.isOverUI();
        bool isBuilding = Tool.getSelected() == Tool.selection.BUILD;

        if(!ui && isBuilding)
        {
            getChild().GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 0, 0.8f));
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

    private GameObject getChild()
    {
        return transform.GetChild(0).gameObject;
    }

    private void resetColor()
    {
        getChild().GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
    }
}
