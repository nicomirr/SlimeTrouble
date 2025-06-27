using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour, IEnemyAttack       
{
    [SerializeField] private bool isBoss;

    [SerializeField] private int specialAttackChance = 10;
    [SerializeField] private Transform[] specialAttackDirections;

    [SerializeField] private Transform firingPoint;

    [SerializeField] private EnemyBullet[] bullets;

    

    private EnemyAI enemyAI;

    private AttackType attackType;

    private bool isAttacking;
    private bool canAttack;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        canAttack = true;    
        attackType = AttackType.NormalAttack;
    }

    public void EnableAttackStateRelayAnimEvent()
    {
        isAttacking = true;
    }

    public void PerformAttackRelayAnimEvent()
    {
        if (!canAttack) return;

        if(isBoss)
            RandomAttackType();

        if(attackType == AttackType.NormalAttack)        
            PerformNormalAttack(enemyAI.Target.position);        
        else 
            PerformSpecialAttack();      

    }    

    private void RandomAttackType()
    {
        int randomResult = Random.Range(1, 101);

        if(randomResult <= specialAttackChance)
            attackType = AttackType.SpecialAttack;
        else
            attackType = AttackType.NormalAttack;

    }

    private void PerformNormalAttack(Vector3 target)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullets[i].transform.position = this.transform.position;
                bullets[i].gameObject.SetActive(true);

                Vector2 targetDirection = (target - firingPoint.position).normalized;
                bullets[i].LaunchBullet(targetDirection);

                break;
            }
        }
    }

    private void PerformSpecialAttack()
    {
        foreach(Transform direction in specialAttackDirections)
        {
            PerformNormalAttack(direction.position);
        }
    }

    public void DisableAttackStateRelayAnimEvent()
    {       
        isAttacking = false;
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
         
}
