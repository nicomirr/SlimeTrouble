using UnityEngine;

public class EnemyAnimationController 
{
    private float idlingSpeed;

    private static readonly int deathHash = Animator.StringToHash("death");
    private static readonly int lookingUpHash = Animator.StringToHash("lookingUp");
    private static readonly int isAttackingHash = Animator.StringToHash("isAttacking");

    private Animator enemyAnimator;
    private EnemyAI enemy;
    private SpriteRenderer enemySpriteRenderer;
    private GameObject enemyAttackArea;

    private float baseAttackAreaScale;

    public EnemyAnimationController(Animator animator, EnemyAI enemy, SpriteRenderer spriteRenderer, GameObject attackArea, float minRandomSpeed, float maxRandomSpeed)
    {
        enemyAnimator = animator;
        this.enemy = enemy;
        enemySpriteRenderer = spriteRenderer;

        if(attackArea != null )
        {
            enemyAttackArea = attackArea;
            baseAttackAreaScale = enemyAttackArea.transform.localScale.x;   
        }

        idlingSpeed = Random.Range(minRandomSpeed, maxRandomSpeed);

    }

    public void FlipEnemy(bool flip) => enemySpriteRenderer.flipX = flip;
    public void FlipEnemyAttackArea(float value) => enemyAttackArea.transform.localScale = new Vector3(baseAttackAreaScale * value, enemyAttackArea.transform.localScale.y);
    public void SetIsLookingUp(bool value) => enemyAnimator.SetBool(lookingUpHash, value);
    public void SetIsAttacking(bool value) => enemyAnimator.SetBool(isAttackingHash, value);
    public void TriggerDeath() => enemyAnimator.SetTrigger(deathHash);
    public void ApplyIdleSpeedToAnimator()
    {
        enemyAnimator.speed = idlingSpeed;
    }
    public void ResetAnimatorSpeed() => enemyAnimator.speed = 1;


   
}
