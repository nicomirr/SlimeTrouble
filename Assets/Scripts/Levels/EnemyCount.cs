using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{   
    [SerializeField] private int totalEnemiesInRoom;

    public int TotalEnemiesInRoom => totalEnemiesInRoom;

    private void Awake()
    {
        EnemyEventManager.OnEnemyDeath += SustractEnemy;
    }

    private void SustractEnemy()
    {    
        totalEnemiesInRoom--;

        if (totalEnemiesInRoom <= 0)
            LevelEventManager.RaiseAllEnemiesDestroyed();
    }    

    private void OnDestroy()
    {
        EnemyEventManager.OnEnemyDeath -= SustractEnemy;
    }
}
