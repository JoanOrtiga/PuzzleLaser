using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaserActive : MonoBehaviour
{
    [HideInInspector] public bool laserHit = false;
    public GameObject children;
    void Update()
    {
        if (laserHit)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
            //com funciona getcomponent in chindren?
            children.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
        }
        else
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
            children.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
        }
    }
}
