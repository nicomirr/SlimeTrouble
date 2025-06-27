using UnityEngine;

public class PlayerAttackingState : PlayerAnimationsBaseState
{
    public PlayerAttackingState(PlayerAnimationController playerAnimationController) : base(playerAnimationController)
    {
        this.playerAnimationController = playerAnimationController;
    }

    public override void EnterState(PlayerStateManager player)
    {
        player.PlayerMover.StopMovement();
        player.PlayerMover.enabled = false; 
        
        playerAnimationController.TriggerAttack();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.PlayerAttacker.IsAttacking) return;

        player.PlayerMover.enabled = true;
        player.PlayerControls.ForceReapplyMovementInput();

        if (player.PlayerMover.PlayerDirection != Vector2.zero)
        {
            player.SwitchState(player.WalkingState);
            return;
        }

        player.SwitchState(player.IdlingState);

    }

}
