using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDyingState : PlayerAnimationsBaseState
{
    public PlayerDyingState(PlayerAnimationController playerAnimationController) : base(playerAnimationController)
    {
        this.playerAnimationController = playerAnimationController;
    }

    public override void EnterState(PlayerStateManager player)
    {
        playerAnimationController.TriggerDeath();
        player.PlayerMover.StopMovement();
        PlayerController.playerControls.Disable();
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}
