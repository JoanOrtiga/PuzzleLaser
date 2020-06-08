using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoor : MonoBehaviour
{
    [SerializeField] private float lerpPosActive = 1;
    [SerializeField] private float lerpPosDes = 1;
    [SerializeField] private float lerpRot = 1;
    [SerializeField] private float lerpScale = 1;

    [SerializeField] private Vector3 finalPos;
    [SerializeField] private Vector3 finalRot;
    [SerializeField] private Vector3 finalScale;

    private Vector3 initialPos;
    private Vector3 initialRot;
    private Vector3 initialScale;

    public bool changePosition;
    public bool changeRotation;
    public bool changeScale;

    private bool isActive = false;

    private void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation.eulerAngles;
        initialScale = transform.localScale;
    }

    public void ChangeState()
    {
        isActive = !isActive;
    }

    private void Update()
    {
        if (changePosition)
        {
            if (isActive)
            {
                transform.position = Vector3.LerpUnclamped(transform.position, finalPos, lerpPosActive * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.LerpUnclamped(transform.position, initialPos, lerpPosDes * Time.deltaTime);
            }
        }

        if (changeRotation)
        {
            if (isActive)
            {
                transform.position = Vector3.Lerp(transform.position, finalPos, lerpRot);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, finalPos, lerpRot);
            }
        }

        if (changeScale)
        {
            if (isActive)
            {
                transform.position = Vector3.Lerp(transform.localScale, finalScale, lerpScale);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.localScale, initialScale, lerpScale);
            }
        }
    }
}
