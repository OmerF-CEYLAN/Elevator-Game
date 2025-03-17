using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float xInput,yInput,sensitivity;

    [SerializeField]
    Transform playerTransform;

    float xRotation,yRotation;

    float freeLookYRotation;

    [SerializeField]
    float xMax;

    float tempYRotation, tempXRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        xInput = Input.GetAxis("Mouse X")  * Time.deltaTime * sensitivity;
        yInput = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        xRotation -= yInput;
        yRotation += xInput;

        xRotation =  gameObject.CompareTag("FP Cam") ?  Mathf.Clamp(xRotation, -xMax, xMax) : Mathf.Clamp(xRotation, -30f, 40f);

        if(gameObject.CompareTag("TP Cam"))
        {
            transform.parent.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        if (gameObject.CompareTag("TP Cam"))
        {

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                tempXRotation = xRotation;
                tempYRotation = yRotation;
            }
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                freeLookYRotation += xInput;
                transform.parent.localRotation = Quaternion.Euler(xRotation, freeLookYRotation, 0f);
                return;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
               xRotation = tempXRotation;
               yRotation = tempYRotation;
            }

            freeLookYRotation = 0f;
        }

        playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }
}
