using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEventManager 
{
    //Se dispara con metodo en EnemyCount, escucha OpenLevelExitDoor
    public static event Action OnAllEnemiesDestroyed;
        
    public static void RaiseAllEnemiesDestroyed()
    {
        OnAllEnemiesDestroyed?.Invoke();
    }
}
