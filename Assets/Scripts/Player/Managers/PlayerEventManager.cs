using UnityEngine;
using System;

public static class PlayerEventManager
{
    //Se dispara con metodo en PlayerHealth, escucha EnemyAI y GameOverScreen
    public static event Action OnPlayerDead;

    //Se dispara con metodo en PlayerInput, escucha PlayerAttack
    public static event Action OnPlayerAttack;

    //Se dispara con metodo en PlayerAttack, escucha PlayerAudioManager
    public static event Action OnPlayerAttackPerformed;

    //Se dispara con metodo en PlayerHealth, escucha PlayerAudioManager
    public static event Action OnPlayerDamaged;

    //Se dispara con metodo en HealingPotion, escucha PlayerHealth
    public static event Action<int> OnPlayerHealed;
    
    //Se dispara con metodo en PlayerController, escucha PlayerMovement 
    public static event Action<Vector2> OnPlayerMovement;

    //Se dispara con metodo en PlayerInput, escucha Healthbar
    public static event Action OnPlayerDisplayHealthbar;

    //Se dispara con metodo en Healthbar, escucha HPFullText
    public static event Action OnDisplayHPFullText;

    //Se dispara con metodo en PlayerController, escucha OpeningText
    public static event Action OnCloseOpeningText;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnStartPause;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnStopPause;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnTryGoToMainMenu;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnAcceptGoToMainMenu;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnCancelGoToMainMenu;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnGoToInstructions;

    //Se dispara con metodo en PlayerController, escucha PauseManager
    public static event Action OnBackFromInstructions;

    //Se dispara con metodo en PLayerController, escucha GameOverScreen
    public static event Action OnRestart;

    public static void RaisePlayerDead()
    {
        OnPlayerDead?.Invoke();
    }

    public static void RaisePlayerAttack()
    {      
        OnPlayerAttack?.Invoke();
    }

    public static void RaisePlayPlayerAttackSound()
    {
        OnPlayerAttackPerformed?.Invoke();
    }

    public static void RaisePlayerHealed(int healingPoints)
    {
        OnPlayerHealed?.Invoke(healingPoints);
    }

    public static void RaisePlayerMovement(Vector2 inputDirection)
    {
        OnPlayerMovement?.Invoke(inputDirection);
    }

    public static void RaisePlayPlayerDamageSound()
    {
        OnPlayerDamaged?.Invoke();
    }

    public static void RaiseDisplayPlayerHealthbar()
    {
        OnPlayerDisplayHealthbar?.Invoke();
    }

    public static void RaiseDisplayHPFullText()
    {
        OnDisplayHPFullText?.Invoke();
    }

    public static void RaiseStartPause()
    {        
        OnStartPause?.Invoke();
    }

    public static void RaiseStopPause()
    {
        OnStopPause?.Invoke();
    }

    public static void RaiseTryGoToMainMenu()
    {
        OnTryGoToMainMenu?.Invoke();
    }

    public static void RaiseAcceptGoToMainMenu()
    {
        OnAcceptGoToMainMenu?.Invoke();
    }

    public static void RaiseCancelGoToMainMenu()
    {
        OnCancelGoToMainMenu?.Invoke();
    }

    public static void RaiseGoToInstructions()
    {
        OnGoToInstructions?.Invoke();
    }

    public static void RaiseBackFromInstructions()
    {
        OnBackFromInstructions?.Invoke();
    }

    public static void RaiseCloseOpeningText()
    {
        OnCloseOpeningText?.Invoke();
    }

    public static void RaiseOnRestart()
    {
        OnRestart?.Invoke();
    }
    
}
