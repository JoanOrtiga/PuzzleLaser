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
    private Vector4 Length = new Vector4(1, 1, 1, 1);

    //One activation per shoot
    private bool LaserSaver = false;
    private bool UpdateSaver = false;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    private CheckLaserActive currentCube;


    private GameObject lastActivator = null;
    private bool lastActivatorExist = true;

    BoxCollider boxColl;
    void Start()
    {
        //Get LineRender and ParticleSystem components from current prefab;  
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();

        boxColl = GetComponent<BoxCollider>();

        Laser.useWorldSpace = true;

        lastActivator = null;
    }


    public int reflections;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private Collision lastKnownActivable;

    void Update()
    {
        // boxColl.size = new Vector3(boxColl.size.x, boxColl.size.y, Vector3.Distance(transform.position, hit.point));
        // boxColl.center = new Vector3(boxColl.center.x, boxColl.center.y, Vector3.Distance(transform.position, hit.point)/2);


        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));

        ray = new Ray(transform.position, transform.forward);

        Laser.positionCount = 1;
        Laser.SetPosition(0, transform.position);
        float remainingLength = MaxLength;

        if (Laser != null && UpdateSaver == false)
        {
            for (int i = 0; i < reflections; i++)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
                {
                    Laser.positionCount += 1;
                    Laser.SetPosition(Laser.positionCount - 1, hit.point);
                    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                    Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
                    Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));


                    if (hit.collider.gameObject != lastActivator)
                    {

                        if (lastActivatorExist)
                        {
                            if (hit.collider.tag == "Activable")
                            {
                                hit.collider.GetComponent<CheckLaserActive>().changeState();
                                lastActivator = hit.collider.gameObject;
                                lastActivatorExist = false;
                            }
                        }
                        else
                        {
                            if(lastActivator.CompareTag("Activable"))
                                lastActivator.GetComponent<CheckLaserActive>().changeState();
                            else if (hit.collider.CompareTag("Activable"))
                                hit.collider.GetComponent<CheckLaserActive>().changeState();
                            lastActivator = hit.collider.gameObject;
                        }
                    }




                    if (hit.collider.tag != "Mirror")
                    {
                        HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                        //Hit effect zero rotation
                        HitEffect.transform.rotation = Quaternion.identity;
                        foreach (var AllPs in Effects)
                        {
                            if (!AllPs.isPlaying) AllPs.Play();
                        }
                        break;
                    }

                }
                else
                {
                    Laser.positionCount += 1;
                    Laser.SetPosition(Laser.positionCount - 1, ray.origin + ray.direction * remainingLength);

                    foreach (var AllPs in Hit)
                    {
                        if (AllPs.isPlaying) AllPs.Stop();
                    }

                }
            }

            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }
        }
    }
}
