using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class ConfigureInputModule : MonoBehaviour
{
    private InputSystemUIInputModule inputModule;

    private void Awake()
    {
        inputModule = GetComponent<InputSystemUIInputModule>();

        if (!Application.isMobilePlatform)
            ConfigureForPC();
    }

    private void ConfigureForPC()
    {
        inputModule.point = null;
        inputModule.leftClick = null;
        inputModule.middleClick = null;
        inputModule.rightClick = null;
        inputModule.scrollWheel = null;
    }
}
