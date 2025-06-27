using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingToTitleAnimEvent : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private void GoToMainMenuAnimEvent()
    {
        sceneLoader.GoToTitleScene();
    }
}
