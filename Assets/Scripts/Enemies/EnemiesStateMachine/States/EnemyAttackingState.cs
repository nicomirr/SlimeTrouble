using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAnimationController.ResetAnimatorSpeed();
        enemy.EnemyAI.StopNavigation();
        enemy.EnemyAnimationController.SetIsAttacking(true);
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        enemy.EnemyAnimationController.FlipEnemy((enemy.EnemyAI.Target.transform.position.x - enemy.transform.position.x) < 0);                
        enemy.EnemyAnimationController.SetIsLookingUp((enemy.EnemyAI.Target.transform.position.y - enemy.transform.position.y) > 0.2);
                
        if (enemy.EnemyAI.IsFleeingDistance() && enemy.EnemyFlee != null)
        {
            enemy.EnemyAnimationController.SetIsAttacking(false);
            enemy.EnemyHealth.ResetIsDamaged();
            enemy.SwitchState(enemy.FleeingState);
            return;
        }

        if (enemy.EnemyAttack.GetIsAttacking()) return;
                
        enemy.EnemyAnimationController.SetIsAttacking(false);
        enemy.EnemyHealth.ResetIsDamaged();

        enemy.SwitchState(enemy.ChasingState);
    }   
   
}
