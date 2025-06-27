using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimationsBaseState : IPlayerState
{
    protected PlayerAnimationController playerAnimationController;

    public PlayerAnimationsBaseState(PlayerAnimationController playertAnimatioController)
    {
        this.playerAnimationController = playertAnimatioController;
    }

    public abstract void EnterState(PlayerStateManager player);

    public abstract void UpdateState(PlayerStateManager player);

}
