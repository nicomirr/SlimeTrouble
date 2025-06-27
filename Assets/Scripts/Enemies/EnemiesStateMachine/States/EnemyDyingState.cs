using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAI.StopNavigation();
        enemy.EnemyAI.DisableAgent();
        enemy.EnemyAnimationController.TriggerDeath();

        if (enemy.EnemiesOnDeathActivation != null)
            enemy.EnemiesOnDeathActivation.EnemyActivationOnDeath();
    }  

    public void UpdateState(EnemyStateManager enemy) {}
       
}
