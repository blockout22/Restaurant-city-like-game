using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{

    public PlaceableObject.TYPE type;
    public enum TYPE {
        CHAIR, TABLE
    }

    void Update()
    {
        if(type == TYPE.CHAIR)
        {
            //check for table
        }
        //else if(type == TYPE.TABLE)
        {
            //check for chair
        }
    }
}
