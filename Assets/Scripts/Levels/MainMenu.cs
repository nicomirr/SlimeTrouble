using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private PlayerDefaultStats playerDefaultStats;

    [SerializeField] private GameObject[] mainMenuOptions;
    [SerializeField] private GameObject[] mainMenuArrows;

    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    private InstructionsInputMainMenu instructionsInput;

    private GameObject currentSelected;

    private void Awake()
    {
        instructionsInput = new InstructionsInputMainMenu();
    }

    private void OnEnable()
    {
        instructionsInput.Enable();
        instructionsInput.Instructions.GoBack.started += CloseInstructionsMenu;        
    }

    void Start()
    {       
        OpenMainMenu();
    }

    private void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == currentSelected) return;

        currentSelected = selected;

        for(int i = 0; i < mainMenuOptions.Length; i++)
        {
            bool isSelected = mainMenuOptions[i] == selected;
            mainMenuArrows[i].SetActive(isSelected);
        }
    }

    private void OpenMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    private void OpenInstructionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    private void CloseAllMenus()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void BeginButtonPressed()
    {
        if (instructions.activeInHierarchy) return;

        PlayerData.ResetToDefaultStats(playerDefaultStats);
        SceneLoader.Instance.LoadNextScene();
    }

    public void InstructionsButtonPressed()
    {
        if (instructions.activeInHierarchy) return;

        instructions.SetActive(true);
    }

    private void CloseInstructionsMenu(InputAction.CallbackContext ctx)
    {
        if (!instructions.activeInHierarchy) return;

        instructions.SetActive(false);

        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);

    }

    private void OnDisable()
    {
        instructionsInput.Disable();
        instructionsInput.Instructions.GoBack.started -= CloseInstructionsMenu;
    }
}
