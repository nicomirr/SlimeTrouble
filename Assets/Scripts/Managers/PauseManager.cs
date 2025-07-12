using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject goToMainMenuScreen;
    [SerializeField] private GameObject instructionsScreen;
    [SerializeField] private GameObject mobileGameplayInput;

    [SerializeField] private GameObject pauseOptions;

    public static PauseManager instance {get; private set;}

    public bool IsPaused { get; private set; }

    private bool goToMainMenuPressed;
    
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerEventManager.OnStartPause += PauseGame;
        PlayerEventManager.OnStopPause += UnpauseGame;

        PlayerEventManager.OnTryGoToMainMenu += AskGoToMainMenu;
        PlayerEventManager.OnAcceptGoToMainMenu += AcceptGoToMainMenu;
        PlayerEventManager.OnCancelGoToMainMenu += CancelGoToMainMenu;

        PlayerEventManager.OnGoToInstructions += GoToInstructions;
        PlayerEventManager.OnBackFromInstructions += BackFromInstructions;        

    }

    private void Start()
    {
        PlayerController.playerControls.Pause.Disable();
    }

    private void PauseGame()
    {       
        PlayerController.playerControls.Actions.Disable();

        PlayerController.playerControls.Pause.Enable();

        if (Application.isMobilePlatform)
            mobileGameplayInput.SetActive(false);

        IsPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        if (goToMainMenuPressed) return;
        if (instructionsScreen.activeInHierarchy) return;

        goToMainMenuPressed = false;

        PlayerController.playerControls.Actions.Enable();
        PlayerController.playerControls.Pause.Disable();

        if (Application.isMobilePlatform)
            mobileGameplayInput.SetActive(true);

        IsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void AskGoToMainMenu()
    {
        if (goToMainMenuPressed) return;
        if (instructionsScreen.activeInHierarchy) return;

        goToMainMenuPressed = true;
        goToMainMenuScreen.SetActive(true);
    }

    private void AcceptGoToMainMenu()
    {
        if (!goToMainMenuPressed) return;


        goToMainMenuPressed = false;
        UnpauseGame();
        SceneLoader.Instance.GoToTitleScene();
    }

    private void CancelGoToMainMenu()
    {
        if (!goToMainMenuPressed) return;

        goToMainMenuScreen.SetActive(false);
        goToMainMenuPressed = false;
    }

    private void GoToInstructions()
    {
        if (goToMainMenuPressed) return;
        if (instructionsScreen.activeInHierarchy) return;
        pauseOptions.SetActive(false);
        instructionsScreen.SetActive(true);
    }

    private void BackFromInstructions()
    {
        if (!instructionsScreen.activeInHierarchy) return;
        instructionsScreen.SetActive(false);
        pauseOptions.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerEventManager.OnStartPause -= PauseGame;
        PlayerEventManager.OnStopPause -= UnpauseGame;

        PlayerEventManager.OnTryGoToMainMenu -= AskGoToMainMenu;
        PlayerEventManager.OnAcceptGoToMainMenu -= AcceptGoToMainMenu;
        PlayerEventManager.OnCancelGoToMainMenu -= CancelGoToMainMenu;

        PlayerEventManager.OnGoToInstructions -= GoToInstructions;
        PlayerEventManager.OnBackFromInstructions -= BackFromInstructions;
    }
}
