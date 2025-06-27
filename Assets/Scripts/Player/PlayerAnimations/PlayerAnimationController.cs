using UnityEngine;

public class PlayerAnimationController 
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private static readonly int isMovingHash = Animator.StringToHash("isMoving");
    private static readonly int isLookingBackHash = Animator.StringToHash("isLookingBack");
    private static readonly int attackHash = Animator.StringToHash("attack");
    private static readonly int dieHash = Animator.StringToHash("die");

    public PlayerAnimationController(Animator animator, SpriteRenderer spriteRenderer)
    {
        this.animator = animator;       
        this.spriteRenderer = spriteRenderer;
    }

    public void FlipPlayer(float value) => spriteRenderer.transform.localScale = new Vector3(value, spriteRenderer.transform.localScale.y);

    public void SetIsMoving(bool value) => animator.SetBool(isMovingHash, value);
    public void SetIsLookingBack(bool value) => animator.SetBool(isLookingBackHash, value);
    public void TriggerAttack() => animator.SetTrigger(attackHash);
    public void TriggerDeath() => animator.SetTrigger(dieHash);
}
