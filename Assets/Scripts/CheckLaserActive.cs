using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaserActive : MonoBehaviour
{
    [HideInInspector] public bool laserHit = false;
    public GameObject[] children;
    void Update()
    {
        if (laserHit)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);

            foreach (GameObject item in children)
            {
                item.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);

            foreach (GameObject item in children)
            {
                item.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
            }
        }

        laserHit = false;
    }
}
