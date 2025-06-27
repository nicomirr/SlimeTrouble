using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyKnockback.Knockback(enemy.EnemyHealth.DamageDirection);
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.EnemyKnockback.KnockingBack) return;

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

        if (enemy.EnemyAI.IsFleeingDistance() && enemy.EnemyFlee != null)
        {
            enemy.EnemyAnimationController.SetIsAttacking(false);
            enemy.EnemyHealth.ResetIsDamaged();
            enemy.SwitchState(enemy.FleeingState);
        }

        if (enemy.EnemyAttack.GetCanAttack())
        {
            if (enemy.EnemyFlee == null)
            {
                if (enemy.EnemyAI.IsNearToTarget())
                {
                    enemy.SwitchState(enemy.AttackingState);
                    return;
                }
            }
            else
            {
                if (enemy.EnemyAI.IsNearToTargetWithFleeing())
                {
                    enemy.SwitchState(enemy.AttackingState);
                    return;
                }
            }
        }

        enemy.SwitchState(enemy.ChasingState);
          
    }

}
