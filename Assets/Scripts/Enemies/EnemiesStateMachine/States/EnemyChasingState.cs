using UnityEngine;

public class EnemyChasingState : IEnemyState
{    
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAnimationController.ApplyIdleSpeedToAnimator();
        enemy.EnemyAI.ResumeNavigation();
    }

    public void UpdateState(EnemyStateManager enemy)
    {      
        
        enemy.EnemyAnimationController.FlipEnemy((enemy.EnemyAI.Target.transform.position.x - enemy.transform.position.x) < 0);

        if(enemy.EnemyAttackArea != null) 
            enemy.EnemyAnimationController.FlipEnemyAttackArea(Mathf.Sign(enemy.EnemyAI.Target.transform.position.x - enemy.transform.position.x));


        enemy.EnemyAnimationController.SetIsLookingUp((enemy.EnemyAI.Target.transform.position.y - enemy.transform.position.y) > 0.2);

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

        if(enemy.EnemyAttack.GetCanAttack())
        {
            if(enemy.EnemyFlee == null)
            {
                if(enemy.EnemyAI.IsNearToTarget())
                {
                    enemy.SwitchState(enemy.AttackingState);
                    return;
                }
            }
            else
            {
                if(enemy.EnemyAI.IsNearToTargetWithFleeing())
                {
                    enemy.SwitchState(enemy.AttackingState);
                    return;
                }
            }
        }

        enemy.EnemyAI.UpdateDestinationToTarget();       
    }
    
}


