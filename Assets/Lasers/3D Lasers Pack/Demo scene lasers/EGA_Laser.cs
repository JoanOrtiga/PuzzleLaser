using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class EGA_Laser : MonoBehaviour
{
    public GameObject HitEffect;
    public float HitOffset = 0;

    public float MaxLength;
    private LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);

    //One activation per shoot
    private bool LaserSaver = false;
    private bool UpdateSaver = false;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    private CheckLaserActive currentCube;
    //EGA_Laser reflectedLaser = null;


    BoxCollider boxColl;
    void Start ()
    {
        //Get LineRender and ParticleSystem components from current prefab;  
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();

        boxColl = GetComponent<BoxCollider>();

        Laser.useWorldSpace = true;
    }

    void Update()
    {
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        //To set LineRender position
        if (Laser != null && UpdateSaver == false)
        {
            Laser.SetPosition(0, transform.position);
            RaycastHit hit; 
       
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
            {
               

                //End laser position if collides with object
                Laser.SetPosition(1, hit.point);
                HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                //Hit effect zero rotation
                HitEffect.transform.rotation = Quaternion.identity;
                foreach (var AllPs in Effects)
                {
                    if (!AllPs.isPlaying) AllPs.Play();
                }
                //Texture tiling
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));
                //Check if hit reflexion
                //CheckReflexion(hit);
                //Check if hit is an end
                CheckActivate(hit);
            }
            else
            {
                //End laser position if doesn't collide with object
                var EndPos = transform.position + transform.forward * MaxLength;
                Laser.SetPosition(1, EndPos);
                HitEffect.transform.position = EndPos;
                foreach (var AllPs in Hit)
                {
                    if (AllPs.isPlaying) AllPs.Stop();
                }
                //Texture tiling
                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, EndPos));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, EndPos));
            }

            //Insurance against the appearance of a laser in the center of coordinates!
            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }

            boxColl.size = new Vector3(boxColl.size.x, boxColl.size.y, Vector3.Distance(transform.position, hit.point));
            boxColl.center = new Vector3(boxColl.center.x, boxColl.center.y, Vector3.Distance(transform.position, hit.point)/2);

            print(hit.collider.transform.tag);
        }  
    }


    /*
    private void CheckReflexion(RaycastHit hit)
    {
        if (hit.collider.CompareTag("ReflexionCube"))
        {
            if (reflectedLaser == null)
            {
                reflectedLaser = Instantiate(this, hit.point + hit.normal * HitOffset, hit.collider.transform.rotation);
            }
        }
        else if (reflectedLaser != null)
        {
            Destroy(reflectedLaser);
        }
    }
    */
    private void CheckActivate(RaycastHit hit)
    {
        CheckLaserActive finalCube = hit.collider.GetComponent<CheckLaserActive>();
        if (finalCube != null)
        {
            if (currentCube == null)
            {
                currentCube = finalCube;
            }
            if (currentCube == finalCube)
            {
                finalCube.laserHit = true;
            }
        }
        else if (currentCube != null)
        {
            currentCube.laserHit = false;
            currentCube = null;
        }
    }

    public void DisablePrepare()
    {
        if (Laser != null)
        {
            Laser.enabled = false;
        }
        UpdateSaver = true;
        //Effects can = null in multiply shooting
        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
}
