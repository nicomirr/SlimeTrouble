using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;

    [SerializeField] private int damage = 4;

    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private bool multipleAttack;

    [SerializeField] private BoxCollider2D attackAreaSides;
    [SerializeField] private BoxCollider2D attackAreaUp;
    [SerializeField] private BoxCollider2D attackAreaDown;

    private Vector2 playerDirection;

    public bool IsAttacking { get; private set; }

    private bool canAttack = true;

    private void Awake()
    {
        PlayerEventManager.OnPlayerMovement += UpdatePlayerDirection;
        PlayerEventManager.OnPlayerAttack += PerformAttack;
    }

    private void UpdatePlayerDirection(Vector2 input)
    {
        playerDirection = input;
    }
        
    private void PerformAttack()    
    {
        if (!canAttack) return;
        StartCoroutine(AttackCooldownRoutine());
        
        IsAttacking = true;

        PlayerAttackSound();      

        BoxCollider2D currentAttackArea = GetCurrentAttackArea();

        Vector2 center = currentAttackArea.bounds.center;
        Vector2 size = currentAttackArea.bounds.size;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f);
        IOrderedEnumerable<Collider2D> sortedHits = hits.OrderBy(hit => Vector2.Distance(hit.transform.position, center));


        foreach (Collider2D hit in sortedHits)
        {
            if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                Vector2 direction = (Vector2)hit.transform.position - center;
                RaycastHit2D obstruction = Physics2D.Raycast(center, direction.normalized, direction.magnitude, obstacleMask);

                if(obstruction.collider == null)
                {                    
                    if (enemy.TryGetComponent<EnemyKnockback>(out EnemyKnockback knockback) && !knockback.KnockingBack)
                    {
                        Vector2 attackDirection = (enemy.transform.position - this.transform.position).normalized;
                        enemy.TakeDamage(damage, attackDirection);          

                        if (!multipleAttack) return; 
                    }                                 
                }
            }         
            
            if(hit.TryGetComponent<BreakableBox>(out BreakableBox breakableBox))
            {
                Vector2 attackDirection = (breakableBox.transform.position - this.transform.position).normalized;
                breakableBox.Shake(attackDirection);
            }
        }
    }
       
    private IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    private void PlayerAttackSound()
    {
        PlayerEventManager.RaisePlayPlayerAttackSound();
    }

    private BoxCollider2D GetCurrentAttackArea()
    {
        if (playerDirection.y == 0)
        {
            return attackAreaSides;
            
        }
        else if (playerDirection.y > 0)
        {
            return attackAreaUp;
        }

        return attackAreaDown;     

    }
        
    public void OnAttackFinishedAnimationEventRelay()
    {
        IsAttacking = false;
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnPlayerMovement -= UpdatePlayerDirection;
        PlayerEventManager.OnPlayerAttack -= PerformAttack;
    }    

}
