using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCameraScript : MonoBehaviour
{
    public GameObject plane;

    Vector3 aradakiMesafe;

    private void Start()
    {
        aradakiMesafe = gameObject.transform.position - plane.transform.position;


    }

    private void LateUpdate()
    {
        gameObject.transform.position = plane.transform.position + aradakiMesafe/2;
    }
}
