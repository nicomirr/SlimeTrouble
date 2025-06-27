using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeingState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAnimationController.ApplyIdleSpeedToAnimator();
        enemy.EnemyAI.ResumeNavigation();
        enemy.EnemyAI.FleeRadially();
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("entra");

        enemy.EnemyAnimationController.FlipEnemy((enemy.EnemyAI.fleeSpot.x - enemy.transform.position.x) < 0);

        enemy.EnemyAnimationController.SetIsLookingUp((enemy.EnemyAI.fleeSpot.y - enemy.transform.position.y) > 0.2);

        if (enemy.EnemyHealth.Health <= 0)
        {
            enemy.SwitchState(enemy.DyingState);
            return;
        }

        if (enemy.EnemyAI.targetDestroyed)
        {
            enemy.SwitchState(enemy.TargetDestroyedState);
            return;
        }

        if (enemy.EnemyHealth.IsDamaged)
        {
            enemy.EnemyHealth.ResetIsDamaged();
            enemy.SwitchState(enemy.DamagedState);
            return;
        }

        if (enemy.EnemyAI.IsFarFromTarget())
        {
            enemy.SwitchState(enemy.IdlingState);
            return;
        }

        if (enemy.EnemyAI.IsNearToTargetWithFleeing() && enemy.EnemyAttack.GetCanAttack())
        {
            enemy.SwitchState(enemy.AttackingState);
            return;
        }      
                     
        enemy.EnemyAI.UpdateDestinationToFleePoint();
    }
}
