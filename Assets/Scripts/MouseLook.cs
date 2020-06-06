using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    [Range(-90,90)]public float maxY, minY;
    public float rotationSmooth;

    public Transform playerCamera;
    float bodyX, mouseY;
    private void Start()
    {
        LockRotation();
    }
    private void Update()
    {
        //Get camera and player rotation
        bodyX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Stop rotating y
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        //Rotation targets
        Quaternion camTargetRotation = Quaternion.Euler(-mouseY, 0, 0);
        Quaternion bodyPlayerRotation = Quaternion.Euler(0, bodyX, 0);

        //Handle rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyPlayerRotation, Time.deltaTime * rotationSmooth);
        playerCamera.localRotation = Quaternion.Lerp(playerCamera.localRotation, camTargetRotation, Time.deltaTime * rotationSmooth);
    }
    void LockRotation()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
