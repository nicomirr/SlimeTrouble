using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetDestroyedState : IEnemyState
{
    public void EnterState(EnemyStateManager enemy)
    {
        enemy.EnemyAI.StopPathfinding();
        enemy.EnemyAI.StopNavigation();
    }

    public void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
