using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Material material;

    public TileInfo(Material material)
    {
        this.material = material;
    }

    public Material getMaterial() {
        return material;
    }
}
