
using UnityEngine;

namespace Assets.Scripts
{
    public class WalkingState : PlayerState
    {

        public override void EnterState(PlayerController player)
        {
            Debug.Log("Entering Walking State");
        }

        public override void ExitState(PlayerController player)
        {
            Debug.Log("Exiting Walking State");
        }

        public override void UpdateState(PlayerController player)
        {
            player.movementService.HandleWalking();

            if (player.IsIdle() && player.isGrounded)
            {
                player.ChangeState(new IdleState());
            }
            else if(player.IsRunning() && player.isGrounded)
            {
                player.ChangeState(new RunningState());
            }
            else if (player.IsJumping() && player.isGrounded)
            {
                player.ChangeState(new JumpingState());
            }
        }
    }
}
