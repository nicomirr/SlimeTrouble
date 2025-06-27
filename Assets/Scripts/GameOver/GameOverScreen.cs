using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject GameOverOptions;
    [SerializeField] private GameObject goToMainMenuScreen;

    [SerializeField] private float gameOverScreenActivationDelayTime = 1.5f;
    [SerializeField] private float gameOverOptionsActivationDelayTime = 1.3f;
    [SerializeField] private GameObject gameOverScreen;

    private bool goToMainMenuPressed;


    private void Awake()
    {
        PlayerEventManager.OnPlayerDead += EnableGameOverScreen;
        PlayerEventManager.OnRestart += Retry;
        PlayerEventManager.OnTryGoToMainMenu += AskGoToMainMenu;
        PlayerEventManager.OnAcceptGoToMainMenu += AcceptGoToMainMenu;
        PlayerEventManager.OnCancelGoToMainMenu += CancelGoToMainMenu;
    }

    private void EnableGameOverScreen()
    {
        StartCoroutine(GameOverScreenRoutine());
    }

    private IEnumerator GameOverScreenRoutine()
    {
        yield return new WaitForSeconds(gameOverScreenActivationDelayTime);
        gameOverScreen.SetActive(true);

        yield return new WaitForSeconds(gameOverOptionsActivationDelayTime);

        GameOverOptions.SetActive(true);
        PlayerController.playerControls.GameOver.Enable();
    }

    private void Retry()
    {
        if (goToMainMenuPressed) return;
        if (PlayerData.CurrentLives <= 0) return;
                
        PlayerData.SustractLives();
        PlayerController.playerControls.GameOver.Disable();
        SceneLoader.Instance.RestartScene();
    }

    private void AskGoToMainMenu()
    {
        if (goToMainMenuPressed) return;

        goToMainMenuPressed = true;
        goToMainMenuScreen.SetActive(true);
        GameOverOptions.SetActive(false);
    }

    private void AcceptGoToMainMenu()
    {
        if (!goToMainMenuPressed) return;

        goToMainMenuPressed = false;
        SceneLoader.Instance.GoToTitleScene();
        PlayerController.playerControls.GameOver.Disable();
    }

    private void CancelGoToMainMenu()
    {
        if (!goToMainMenuPressed) return;

        GameOverOptions.SetActive(true);
        goToMainMenuScreen.SetActive(false);
        goToMainMenuPressed = false;
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnPlayerDead -= EnableGameOverScreen;
        PlayerEventManager.OnRestart -= Retry;
        PlayerEventManager.OnTryGoToMainMenu -= AskGoToMainMenu;
        PlayerEventManager.OnAcceptGoToMainMenu -= AcceptGoToMainMenu;
        PlayerEventManager.OnCancelGoToMainMenu -= CancelGoToMainMenu;
    }
}
