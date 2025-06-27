using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{      
    public static PlayerControls playerControls {get; private set;}

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Pause.Disable();
        playerControls.GameOver.Disable();

        playerControls.Actions.Movement.performed += UpdatePlayerDirection;
        playerControls.Actions.Movement.canceled += UpdatePlayerDirection;
        playerControls.Actions.Attack.started += OnAttackPressed;
        playerControls.Actions.DisplayHealthbar.started += OnDisplayHealthbarPressed;
        playerControls.Actions.StartPause.started += OnStartPause;

        playerControls.Pause.StopPause.started += OnStopPause;
        playerControls.Pause.GoToMainMenu.started += OnTryGoToMainMenu;
        playerControls.Pause.YesButton.started += OnAcceptGoToMainMenu;
        playerControls.Pause.NoButton.started += OnCancelGoToMainMenu;
        playerControls.Pause.GoToInstructions.started += OnGoToInstructions;
        playerControls.Pause.BackFromInstructions.started += OnBackFromInstructions;

        playerControls.GameOver.RestartLevel.started += OnRestartLevel;
        playerControls.GameOver.GoToMainMenu.started += OnTryGoToMainMenu;
        playerControls.GameOver.YesButton.started += OnAcceptGoToMainMenu;
        playerControls.GameOver.NoButton.started += OnCancelGoToMainMenu;
        
        playerControls.Opening.Continue.started += OnCloseOpeningTextButton;              

    }

    private void UpdatePlayerDirection(InputAction.CallbackContext context)
    {
        Vector2 directionInput = context.ReadValue<Vector2>();
        PlayerEventManager.RaisePlayerMovement(directionInput);
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {       
        PlayerEventManager.RaisePlayerAttack();
    }

    private void OnDisplayHealthbarPressed(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseDisplayPlayerHealthbar();
    }

    private void OnCloseOpeningTextButton(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseCloseOpeningText();
    }

    private void OnStartPause(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseStartPause();
    }
    private void OnStopPause(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseStopPause();
    }

    private void OnTryGoToMainMenu(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseTryGoToMainMenu();
    }

    private void OnAcceptGoToMainMenu(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseAcceptGoToMainMenu();
    }

    private void OnCancelGoToMainMenu(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseCancelGoToMainMenu();
    }

    private void OnGoToInstructions(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseGoToInstructions();
    }

    private void OnBackFromInstructions(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseBackFromInstructions();
    }

    private void OnRestartLevel(InputAction.CallbackContext context)
    {
        PlayerEventManager.RaiseOnRestart();
    }

    public void ForceReapplyMovementInput()
    {
        Vector2 currentInput = playerControls.Actions.Movement.ReadValue<Vector2>();
        PlayerEventManager.RaisePlayerMovement(currentInput);
    }

    private void OnDisable()
    {      
        playerControls.Actions.Movement.performed -= UpdatePlayerDirection;
        playerControls.Actions.Movement.canceled -= UpdatePlayerDirection;
        playerControls.Actions.Attack.started -= OnAttackPressed;
        playerControls.Actions.DisplayHealthbar.started -= OnDisplayHealthbarPressed;
        playerControls.Actions.StartPause.started -= OnStartPause;

        playerControls.Pause.StopPause.started -= OnStopPause;
        playerControls.Pause.GoToMainMenu.started -= OnTryGoToMainMenu;
        playerControls.Pause.YesButton.started -= OnAcceptGoToMainMenu;
        playerControls.Pause.NoButton.started -= OnCancelGoToMainMenu;
        playerControls.Pause.GoToInstructions.started -= OnGoToInstructions;
        playerControls.Pause.BackFromInstructions.started -= OnBackFromInstructions;

        playerControls.GameOver.RestartLevel.started -= OnRestartLevel;
        playerControls.GameOver.GoToMainMenu.started -= OnTryGoToMainMenu;
        playerControls.GameOver.YesButton.started -= OnAcceptGoToMainMenu;
        playerControls.GameOver.NoButton.started -= OnCancelGoToMainMenu;

        playerControls.Opening.Continue.started -= OnCloseOpeningTextButton;

        playerControls.Disable();

        playerControls = null;
    }

}

        