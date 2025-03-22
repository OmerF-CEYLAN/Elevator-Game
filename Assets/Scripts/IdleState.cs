using UnityEngine;

namespace Assets.Scripts
{
    public class IdleState : PlayerState
    {
        public override void EnterState(PlayerController player)
        {
            Debug.Log("Entering Idle State");
        }

        public override void ExitState(PlayerController player)
        {
            Debug.Log("Exiting Idle State");
        }

        public override void UpdateState(PlayerController player)
        {
            if(player.IsWalking() && player.isGrounded)
            {
                player.ChangeState(new WalkingState());
            }
            else if(player.IsRunning() && player.isGrounded)
            {
                player.ChangeState(new RunningState());
            }
            else if(player.IsJumping() && player.isGrounded)
            {
                player.ChangeState(new JumpingState());
            }
        }
    }
}
