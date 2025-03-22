using UnityEngine;

namespace Assets.Scripts
{
    public class RunningState : PlayerState
    {

        public override void EnterState(PlayerController player)
        {
            Debug.Log("Entering Running State");
        }

        public override void ExitState(PlayerController player)
        {
            Debug.Log("Exiting Running State");
        }

        public override void UpdateState(PlayerController player)
        {
            player.movementService.HandleRunning();

            if (player.IsIdle() && player.isGrounded)
            {
                player.ChangeState(new IdleState());
            }
            else if(player.IsWalking() && player.isGrounded)
            {
                player.ChangeState(new WalkingState());
            }
            else if (player.IsJumping() && player.isGrounded)
            {
                player.ChangeState(new JumpingState());
            }
        }
    }
}
