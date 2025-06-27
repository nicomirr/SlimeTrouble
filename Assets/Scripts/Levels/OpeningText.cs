using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpeningText : MonoBehaviour
{
    private void Awake()
    {
        PlayerEventManager.OnCloseOpeningText += CloseMessage;
    }

    void Start()
    {
        PlayerController.playerControls.Actions.Disable();
        PlayerController.playerControls.Opening.Enable();
    }

    private void CloseMessage()
    {
        PlayerController.playerControls.Actions.Enable();
        PlayerController.playerControls.Opening.Disable();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerEventManager.OnCloseOpeningText -= CloseMessage;
    }

}
