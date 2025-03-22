using UnityEngine;

namespace Assets.Scripts
{
    public class JumpingState : PlayerState
    {

        public override void EnterState(PlayerController player)
        {
            Debug.Log("Entering Jumping State");


        }

        public override void ExitState(PlayerController player)
        {
            Debug.Log("Exiting Jumping State");
        }

        public override void UpdateState(PlayerController player)
        {
            player.movementService.HandleJumping();

            if (player.IsIdle() && player.isGrounded)
            {
                player.ChangeState(new IdleState());
            }
            else if(player.IsWalking() && player.isGrounded)
            {
                player.ChangeState(new WalkingState());
            }
            else if(player.IsRunning() && player.isGrounded)
            {
                player.ChangeState(new RunningState());
            }
        }
    }
}
