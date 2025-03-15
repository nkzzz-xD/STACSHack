using UnityEngine;
using Ststem.Collections;
using Systme.Collections.Generic;

public class PlayerCam : MonoBehaviour
{
    public floatr sensX
    public float sensY;

    public Transform orientation;

    private void Start()
    {
        Cursor.lockState = cursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        tranform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
