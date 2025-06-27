using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private float knockbackPower = 3;

    [SerializeField] private float attackCooldown;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private BoxCollider2D attackArea;

    [SerializeField] private int damage;

    private bool isAttacking;
    private bool canAttack;

    private void Start()
    {
        canAttack = true;
    }

    public void EnableAttackStateRelayAnimEvent()
    {
        isAttacking = true;
    }

    public void PerformAttackRelayAnimEvent()
    {        
        StartCoroutine(AttackCooldownRoutine());

        Vector2 center = attackArea.bounds.center;
        Vector2 size = attackArea.bounds.size;
                
        Collider2D hit = Physics2D.OverlapBox(center, size, 0f, playerLayer);
        if (hit == null) return;

        if (hit.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            if (hit.TryGetComponent<PlayerKnockback>(out PlayerKnockback knockback))
                knockback.SetIncomingKnockbackStrength(knockbackPower);

            Vector2 attackDirection = (player.transform.position - this.transform.position).normalized;
            player.TakeDamage(damage, attackDirection);           
            
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public void DisableAttackStateRelayAnimEvent()
    {
        isAttacking = false;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

}
