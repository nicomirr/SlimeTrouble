using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;

    private PlayerAnimationController animationController;

    public PlayerIdlingState IdlingState { get; private set; }
    public PlayerWalkingState WalkingState { get; private set; }
    public PlayerAttackingState AttackingState { get; private set; }
    public PlayerDamagedState DamagedState { get; private set; }
    public PlayerDyingState DyingState { get; private set; }


    private Animator PlayerAnimator;
    private SpriteRenderer PlayerSpriteRenderer;
    public PlayerController PlayerControls { get; private set; }
    public PlayerMovement PlayerMover {  get; private set; }
    public PlayerAttack PlayerAttacker { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerKnockback PlayerKnockback { get; private set; }
   

    private void Awake()
    {       
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        PlayerControls = GetComponent<PlayerController>();
        PlayerMover = GetComponent<PlayerMovement>();
        PlayerAttacker = GetComponent<PlayerAttack>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerKnockback = GetComponent<PlayerKnockback>();

        animationController = new PlayerAnimationController(PlayerAnimator, PlayerSpriteRenderer);

        IdlingState = new PlayerIdlingState(animationController);
        WalkingState = new PlayerWalkingState(animationController);
        AttackingState = new PlayerAttackingState(animationController);
        DamagedState = new PlayerDamagedState(animationController);
        DyingState = new PlayerDyingState(animationController);
    }

    private void Start()
    {
        currentState = IdlingState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }        

    public void SwitchState(IPlayerState state)
    {
        currentState = state;
        state.EnterState(this);
    }   
}
