using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCyle : MonoBehaviour
{
    [Header("Reference Directional Light which will be rotated to simulate day/night")]
    public Transform lightTransform;
    private float rot = 50;

    [Header("The speed of the rotation of the directional light")]
    public float speed = 0.01f;

    void Start()
    {
        InvokeRepeating("runLights", 5f, .1f);
    }

    void runLights()
    {
        rot += speed;
        lightTransform.rotation = Quaternion.Euler(rot, -30, 0);
    }
}
