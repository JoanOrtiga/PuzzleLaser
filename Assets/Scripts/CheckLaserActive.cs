using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckLaserActive : MonoBehaviour
{
    [HideInInspector] public bool laserHit = false;
    public GameObject[] children;

    public UnityEvent activate;

    public void changeState()
    {
        print(laserHit);
        laserHit = !laserHit;

        
        if (laserHit)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);

            activate.Invoke();

            foreach (GameObject item in children)
            {
                item.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
            }
        }
        else
        {

            activate.Invoke();

            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);

            foreach (GameObject item in children)
            {
                item.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
            }
        }
    }
}
