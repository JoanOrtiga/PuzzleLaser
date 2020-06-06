using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public CheckLaserActive active;
    public List<Animator> doorAinimators;
    private bool activated = false;
    public bool reverse = false;
    void Update()
    {
        if (reverse)
        {
            ReversedCheck();
        }
        else
        {
            NormalCheck();
        }

    }

    private void ReversedCheck()
    {
        if (!active.laserHit && !activated)
        {
            activated = true;
            foreach (Animator a in doorAinimators)
            {
                a.SetBool("Open", true);
            }
        }
        else if (active.laserHit && activated)
        {
            activated = false;
            foreach (Animator a in doorAinimators)
            {
                a.SetBool("Open", false);
            }
        }
    }

    private void NormalCheck()
    {
        if (active.laserHit && !activated)
        {
            activated = true;
            foreach (Animator a in doorAinimators)
            {
                a.SetBool("Open", true);
            }
        }
        else if (!active.laserHit && activated)
        {
            activated = false;
            foreach (Animator a in doorAinimators)
            {
                a.SetBool("Open", false);
            }
        }
    }
}
