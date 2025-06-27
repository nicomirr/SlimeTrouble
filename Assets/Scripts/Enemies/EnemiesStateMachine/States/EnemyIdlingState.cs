using UnityEngine;

public class EnemyIdlingState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAnimationController.ApplyIdleSpeedToAnimator();
        enemy.EnemyAI.StopNavigation();
    }    

    public void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.EnemyHealth.Health <= 0)
        {
            enemy.SwitchState(enemy.DyingState);
            return;
        }

        if (enemy.EnemyAI.CanChaceTarget())
            enemy.SwitchState(enemy.ChasingState);

    }

}
