using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        bool isIdle, isWalking, isRunning;
        public bool isGrounded;

        PlayerState currentState;

        public MovementService movementService;

        [SerializeField]
        Transform groundCheck;

        [SerializeField]
        float distanceToGround;

        [SerializeField]
        LayerMask groundMask;

        [SerializeField]
        Animator animator;

        void Start()
        {
            movementService = GetComponent<MovementService>();
            currentState = new IdleState();
            currentState.EnterState(this);
        }

        void Update()
        {
            CheckGroundContact();
            currentState.UpdateState(this);
            movementService.ApplyGravity();
            UpdateAnimator();
        }

        public void ChangeState(PlayerState newState)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
            UpdateAnimator();
        }

        public bool IsIdle()
        {
            return Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && isGrounded;
        }

        public bool IsWalking()
        {
            return !Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded;
        }

        public bool IsRunning()
        {
             return Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded;
        }

        public bool IsJumping()
        {
            return Input.GetButtonDown("Jump") && isGrounded;
        }

        void CheckGroundContact()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);
        }

        void UpdateAnimator()
        {
            animator.SetBool("isIdle", IsIdle());
            animator.SetBool("isWalking", IsWalking());
            animator.SetBool("isRunning", IsRunning());
            animator.SetBool("isJumping", IsJumping());
        }
    }
}
