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

    [SerializeField]
    float xMax;

    void Start()
    {
        
    }

    
    void Update()
    {
        xInput = Input.GetAxis("Mouse X")  * Time.deltaTime * sensitivity;
        yInput = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        xRotation -= yInput;
        yRotation += xInput;

        playerTransform.rotation = Quaternion.Euler(0f, yRotation,0f);

        xRotation = Mathf.Clamp(xRotation, -xMax, xMax);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }
}
