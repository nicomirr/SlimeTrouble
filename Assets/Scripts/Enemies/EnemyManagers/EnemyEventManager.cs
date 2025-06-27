using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyEventManager
{   
    //Se dispara con metodo en EnemyHealth, escuchan EnemyCount y EntranceBars
    public static event Action OnEnemyDeath;

    public static void RaiseEnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }

}
