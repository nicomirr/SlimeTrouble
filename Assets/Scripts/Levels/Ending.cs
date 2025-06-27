using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject endingScreen;

    private void Awake()
    {
        LevelEventManager.OnAllEnemiesDestroyed += StartEnding;
    }

    private void StartEnding()
    {
        PlayerController.playerControls.Disable();
        LevelMusic.Instance.StopMusic();
        endingScreen.SetActive(true);
    }    

    private void OnDestroy()
    {
        LevelEventManager.OnAllEnemiesDestroyed -= StartEnding;
    }
}
