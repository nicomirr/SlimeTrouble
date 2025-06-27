using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData 
{
    public static int CurrentHealth {get; private set;}
    public static int CurrentLives {get; private set;}

    public static void ResetToDefaultStats(PlayerDefaultStats playerDefaultStats)
    {
        CurrentHealth = playerDefaultStats.maxHealth;
        CurrentLives = playerDefaultStats.maxLives;      
    }

    public static void SetCurrentHealth(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

    public static void SustractLives()
    {
        CurrentLives--;
    }
}
