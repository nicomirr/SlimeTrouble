
using UnityEngine;

public class PlayerWalkingState : PlayerAnimationsBaseState
{    
    public PlayerWalkingState(PlayerAnimationController playerAnimationController) : base(playerAnimationController)
    {
        this.playerAnimationController = playerAnimationController;
    }

    public override void EnterState(PlayerStateManager player)
    {        
        playerAnimationController.SetIsMoving(true);
    }

    public override void UpdateState(PlayerStateManager player)
    {       
        if(player.PlayerHealth.CurrentHealth <= 0)
        {
            player.SwitchState(player.DyingState);
            return;
        }

        if(player.PlayerHealth.IsDamaged)
        {
            player.PlayerHealth.ResetIsDamaged();
            player.SwitchState(player.DamagedState);
            return;
        }
        
        if (player.PlayerAttacker.IsAttacking)
        {
            player.SwitchState(player.AttackingState);
            return;
        }

        if (player.PlayerMover.PlayerDirection == Vector2.zero)
        {
            player.SwitchState(player.IdlingState);
            return;
        }

        if (player.PlayerMover.PlayerDirection.x != 0)
        {
            playerAnimationController.FlipPlayer(Mathf.Sign(player.PlayerMover.PlayerDirection.x));
        }
               
        playerAnimationController.SetIsLookingBack(player.PlayerMover.PlayerDirection.y > 0);        
        
    }

}
