using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float xInput,zInput;

    CharacterController characterController;

    [SerializeField]
    private float walkSpeed,runSpeed;

    [SerializeField]
    float checkRadius;

    bool isGrounded = true;

    [SerializeField]
    LayerMask groundMask;

    float gravity = -9.81f;

    Vector3 velocity;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float jumpHeight,dampingFactor;

    float currentSpeed, currentDampingFactor;

    float elasedTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        xInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime * currentSpeed;
        zInput = Input.GetAxisRaw("Vertical") * Time.deltaTime * currentSpeed;

        if (!isGrounded)
        {
            xInput = zInput = 0f;
        }

        Vector3 moveDirection = transform.TransformDirection(new Vector3(xInput, 0f, zInput));

        characterController.Move(moveDirection);
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            elasedTime = 0f;

            currentDampingFactor = dampingFactor;

            velocity = Vector3.up * (-2);
        }

        if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.W) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity += transform.forward * walkSpeed;
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {

            elasedTime += Time.deltaTime;

            if (elasedTime <= 0.2f && Input.GetKey(KeyCode.W))
            {
                velocity = transform.TransformDirection(new Vector3(0f, velocity.y, walkSpeed));
            }

            velocity.x = Mathf.Lerp(velocity.x, 0f, currentDampingFactor);
            velocity.z = Mathf.Lerp(velocity.z, 0f, currentDampingFactor);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            currentDampingFactor = dampingFactor * 2;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }

}
