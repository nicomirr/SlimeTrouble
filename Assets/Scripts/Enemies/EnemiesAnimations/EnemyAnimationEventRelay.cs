using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventRelay : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private IEnemyAttack enemyAttack;

    private void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyAttack = GetComponentInParent<IEnemyAttack>();
    }

    private void OnDeathAnimationEnded()
    {
        Destroy(enemyHealth.gameObject);
    }

    private void OnAttackStart()
    {
        enemyAttack.EnableAttackStateRelayAnimEvent();
    }

    private void PerformAttack()
    {
        enemyAttack.PerformAttackRelayAnimEvent();
    }

    private void OnAttackFinish()
    {
        enemyAttack.DisableAttackStateRelayAnimEvent();
    }
    
}
