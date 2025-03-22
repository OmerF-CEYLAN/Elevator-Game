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
    float distanceToGround;

    bool isGrounded = true;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float gravity = -50;

    Vector3 velocity;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float jumpHeight,dampingFactor;

    float currentSpeed, currentDampingFactor;

    float elasedTime;

    PlayerSounds playerSounds;

    [SerializeField]
    float lateJumpInterval;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerSounds = GetComponent<PlayerSounds>();
    }

    
    void Update()
    {
        Move();
        Jump();

    }

    private void Move()
    {
        if (!isGrounded)
        {
            return;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, dampingFactor);
            playerSounds.OnRunFootstepDelay();
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, dampingFactor * 2f);
            playerSounds.OnWalkFootstepDelay();
        }


        xInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime * currentSpeed;
        zInput = Input.GetAxisRaw("Vertical") * Time.deltaTime * currentSpeed;

        if(xInput != 0 || zInput != 0) // if you are pressing keys to move
        {
            playerSounds.PlayFootstepSound();
        }

        Vector3 moveDirection = transform.TransformDirection(new Vector3(xInput, 0f, zInput));

        characterController.Move(moveDirection);


    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            elasedTime = 0f;

            currentDampingFactor = dampingFactor;

            velocity = Vector3.up * (-2);
        }

        if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.W) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity += transform.forward * currentSpeed;
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
            elasedTime += Time.deltaTime;

            if (elasedTime <= lateJumpInterval && Input.GetKey(KeyCode.W))
            {
                velocity = transform.TransformDirection(new Vector3(0f, velocity.y, currentSpeed));
            }

            velocity.x = Mathf.Lerp(velocity.x, 0f, currentDampingFactor);
            velocity.z = Mathf.Lerp(velocity.z, 0f, currentDampingFactor);

            if (Input.GetKeyUp(KeyCode.W))
            {
                currentDampingFactor = dampingFactor * 10;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            currentDampingFactor = dampingFactor * 10;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }

}
