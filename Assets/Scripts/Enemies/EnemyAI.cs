using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;


    [SerializeField] private float beginChasingDistance;

    [SerializeField] private float stopChasingDistance;

    [SerializeField] private float stopingAttackDistance;

    [SerializeField] private float beginFleeingDistance;

    public Transform Target {get; private set;}
    public Vector3 fleeSpot {get; private set;}
    public bool targetDestroyed {get; private set;}

    private IFleeingEnemy enemyFlee;

    private NavMeshAgent agent;

    private Transform playerPosition;



    private void Awake()
    {
        enemyFlee = GetComponent<IFleeingEnemy>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    private void OnEnable()
    {
        PlayerEventManager.OnPlayerDead += ValidateTargetDestroyedStatus;
    }

    private void Start()
    {
        playerPosition = FindObjectOfType<PlayerController>().transform;
        Target = playerPosition;
        agent.SetDestination(Target.position);      
    }

    public void StopPathfinding()
    {
        agent.ResetPath();
    }

    public void ResumeNavigation()
    {
        agent.isStopped = false;
    }

    public void StopNavigation()
    {       
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    public void FleeRadially()
    {
        fleeSpot = enemyFlee.TryGetRadialFleePosition(Target.position, DistanceToTarget());
        agent.SetDestination((Vector3)fleeSpot);       
    }
       
    public void DisableAgent()
    {
        agent.enabled = false;
    }

    public bool IsFarFromTarget() => DistanceToTarget() >= stopChasingDistance;
    public bool IsNearToTarget() => DistanceToTarget() <= stopingAttackDistance;
    public bool IsNearToTargetWithFleeing() => DistanceToTarget() <= stopingAttackDistance && DistanceToTarget() > beginFleeingDistance;
    public bool IsFleeingDistance() => DistanceToTarget() <= beginFleeingDistance;

   
    public bool CanChaceTarget() => isTargetInChaseRange() && HasLineOfSightToTarget();

    public void UpdateDestinationToTarget()
    {
        if (!agent.pathPending && !agent.isStopped)
        {
            agent.SetDestination(Target.position);
        }
    }

    public void UpdateDestinationToFleePoint()
    {
        if (!agent.pathPending && !agent.isStopped)
        {
            agent.SetDestination((Vector3)fleeSpot);
        }
    }

    private void SetPlayerAsTarget()
    {
        Target = playerPosition;
    }

    private bool isTargetInChaseRange() => DistanceToTarget() <= beginChasingDistance;
    private float DistanceToTarget()
    {
        return Vector3.Distance(this.transform.position, Target.position);
    }

    private bool HasLineOfSightToTarget()
    {
        Vector2 origin = this.transform.position;
        Vector2 direction = ((Vector2)Target.position - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, beginChasingDistance, obstacleMask);
             
        return hit.collider == null;
    }

    private void ValidateTargetDestroyedStatus()
    {
        targetDestroyed = true;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnPlayerDead -= StopPathfinding;
    }
}
