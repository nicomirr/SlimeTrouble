using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpeningText : MonoBehaviour
{
    [SerializeField] private GameObject mobileGameplayInput;

    private void Awake()
    {
        PlayerEventManager.OnCloseOpeningText += CloseMessage;
    }

    void Start()
    {
        if(Application.isMobilePlatform)
            mobileGameplayInput.SetActive(false);

        PlayerController.playerControls.Actions.Disable();
        PlayerController.playerControls.Opening.Enable();
    }

    private void CloseMessage()
    {
        if (Application.isMobilePlatform)
            mobileGameplayInput.SetActive(true);

        PlayerController.playerControls.Actions.Enable();
        PlayerController.playerControls.Opening.Disable();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerEventManager.OnCloseOpeningText -= CloseMessage;
    }

}
