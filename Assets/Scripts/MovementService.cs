using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovementService : MonoBehaviour // Şuan idle mı running bilmesine gerek yok sadece hareketleri yapacak
    {
        [SerializeField]
        private float walkSpeed, runSpeed;

        CharacterController characterController;

        PlayerController playerController;

        [SerializeField]
        float gravity = -50;

        [SerializeField]
        float jumpForce;

        Vector3 velocity;

        void Awake()
        {
            playerController = GetComponent<PlayerController>();
            characterController = GetComponent<CharacterController>();
        }

        public void HandleWalking()
        {
            Vector3 move = transform.forward * walkSpeed * Time.deltaTime;
            characterController.Move(move);
        }

        public void HandleRunning()
        {
            Vector3 move = transform.forward * runSpeed * Time.deltaTime;
            characterController.Move(move);
        }

        public void HandleJumping()
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            characterController.Move(velocity * Time.deltaTime);
        }

        public void ApplyGravity()
        {
            if (!playerController.IsJumping())
            {
                velocity.y += gravity * Time.deltaTime; // Yerçekimi uygula
            }
            else
            {
                velocity.y = -2f; // Yere yapışmasını sağla (karakterin süzülmesini engellemek için)
            }

            characterController.Move(velocity * Time.deltaTime);
        }
    }
}
