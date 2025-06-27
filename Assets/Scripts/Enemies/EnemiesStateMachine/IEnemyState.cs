using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyStateManager enemy);
    void UpdateState(EnemyStateManager enemy);  

}
