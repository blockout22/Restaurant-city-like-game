using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMap : MonoBehaviour
{
    public static MaterialMap Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Material DEFAULT;
    public Material RED;
    public Material TILE_MAT;
    public Material GRASS;
}
