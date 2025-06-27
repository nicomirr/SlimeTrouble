using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private float minIdlingRandomSpeed;
    [SerializeField] private float maxIdlingRandomSpeed;

    [SerializeField] private GameObject enemyAttackArea;
    public GameObject EnemyAttackArea => enemyAttackArea;

    public EnemyAnimationController EnemyAnimationController {get ; private set;}

    public EnemyIdlingState IdlingState { get; private set; }
    public EnemyChasingState ChasingState { get; private set; }
    public EnemyDamagedState DamagedState { get; private set; }
    public EnemyAttackingState AttackingState { get; private set; }
    public EnemyTargetDestroyedState TargetDestroyedState { get; private set; }
    public EnemyFleeingState FleeingState { get; private set; }
    public EnemyDyingState DyingState { get; private set; }

    public EnemyAI EnemyAI { get; private set; }

    public EnemyHealth EnemyHealth { get; private set; }
    public EnemyKnockback EnemyKnockback { get; private set; }

    public ActivateEnemiesOnDeath EnemiesOnDeathActivation { get; private set; }

    public IFleeingEnemy EnemyFlee { get; private set; }

    public IEnemyAttack EnemyAttack { get; private set; }
    
    private Animator enemyAnimator;
    private SpriteRenderer enemySpriteRenderer;

    private IEnemyState currentState;


    private void Awake()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        EnemyAI = GetComponent<EnemyAI>();
        enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        EnemyAnimationController = new EnemyAnimationController(enemyAnimator, EnemyAI, enemySpriteRenderer, enemyAttackArea, minIdlingRandomSpeed, maxIdlingRandomSpeed);

        EnemyHealth = GetComponent<EnemyHealth>();
        EnemyKnockback = GetComponent<EnemyKnockback>();

        EnemiesOnDeathActivation = GetComponent<ActivateEnemiesOnDeath>();

        EnemyFlee = GetComponent<IFleeingEnemy>();
        EnemyAttack = GetComponent<IEnemyAttack>();

        IdlingState = new EnemyIdlingState();
        ChasingState = new EnemyChasingState();
        DamagedState = new EnemyDamagedState();
        AttackingState = new EnemyAttackingState();
        TargetDestroyedState = new EnemyTargetDestroyedState();
        FleeingState = new EnemyFleeingState();
        DyingState = new EnemyDyingState();
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

    public void SwitchState(IEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
