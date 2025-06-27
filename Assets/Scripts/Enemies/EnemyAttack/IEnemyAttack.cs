using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack
{
    public void EnableAttackStateRelayAnimEvent();

    public void PerformAttackRelayAnimEvent();

    public void DisableAttackStateRelayAnimEvent();

    public bool GetIsAttacking();

    public bool GetCanAttack();
}
