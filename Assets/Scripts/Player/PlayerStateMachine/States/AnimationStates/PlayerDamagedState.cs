using UnityEngine;

public class PlayerDamagedState : PlayerAnimationsBaseState
{
    public PlayerDamagedState(PlayerAnimationController playerAnimationController) : base(playerAnimationController)
    {
        this.playerAnimationController = playerAnimationController;
    }

    public override void EnterState(PlayerStateManager player)
    {
        player.PlayerKnockback.Knockback(player.PlayerHealth.DamageDirection);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.PlayerKnockback.KnockingBack) return;

        if(player.PlayerHealth.CurrentHealth <= 0)
        {
            player.SwitchState(player.DyingState);
            return;
        }

        if(player.PlayerMover.PlayerDirection != Vector2.zero)
        {
            player.SwitchState(player.WalkingState);
            return;
        }

        player.SwitchState(player.IdlingState);
    }

}
