using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBars : Bars
{
    [SerializeField] private EnemyHealth[] roomEnemies;

    private readonly int isEntranceBars = Animator.StringToHash("isEntranceBars");

    protected override void Awake()
    {
        base.Awake();
        EnemyEventManager.OnEnemyDeath += CheckEnemiesAliveStatus;
    }

    private void Start()
    {
        animator.SetBool(isEntranceBars, true);
    }    

    private void CheckEnemiesAliveStatus()
    {
        for (int i = 0; i < roomEnemies.Length; i++)
        {
            if (roomEnemies[i].IsAlive)
                return;
        }
        
        BarsDown();
    }

    private void OnDestroy()
    {
        EnemyEventManager.OnEnemyDeath -= CheckEnemiesAliveStatus;
    }
}
