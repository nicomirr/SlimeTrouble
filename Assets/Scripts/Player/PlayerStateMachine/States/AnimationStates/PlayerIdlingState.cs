using UnityEngine;

public class PlayerIdlingState : PlayerAnimationsBaseState
{
    public PlayerIdlingState(PlayerAnimationController playerAnimationController): base(playerAnimationController)
    {
        this.playerAnimationController = playerAnimationController;
    }

    public override void EnterState(PlayerStateManager player)
    {       
        player.PlayerMover.StopMovement();
        playerAnimationController.SetIsMoving(false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.PlayerHealth.CurrentHealth <= 0)
        {
            player.SwitchState(player.DyingState);
            return;
        }

        if (player.PlayerHealth.IsDamaged)
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

        if (player.PlayerMover.PlayerDirection != Vector2.zero)
        {
            player.SwitchState(player.WalkingState);
            return;
        }
    }
}
