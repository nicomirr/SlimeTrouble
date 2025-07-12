using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlayerUIVisibility : MonoBehaviour
{
    private float displayLivesDelay = 2.8f;

    [SerializeField] private Image playerLivesImage;
    [SerializeField] private TextMeshProUGUI playerLivesText;

    [SerializeField] private SpriteRenderer playerHealthbar;
    [SerializeField] private SpriteRenderer playerHealthbarBackground;

    [SerializeField] private float visibilityTime = 3f;

    private float visibilityTimer;
    private bool visible;


    private void Awake()
    {
        PlayerEventManager.OnPlayerDisplayUI += DisplayUI;
        PlayerEventManager.OnPlayerDead += DisplayLives;
    }

    private void Start()
    {
        ToggleUI(0);
    }

    private void Update()
    {
        if (!visible) return;

        VisibilityTimer();
    }

    private void VisibilityTimer()
    {
        visibilityTimer += Time.deltaTime;
        if (visibilityTimer >= visibilityTime)
        {
            ToggleUI(0);
            visible = false;
        }
    }

    private void DisplayUI()
    {
        visible = true;
        ToggleUI(1);
    }
    private void ToggleUI(float alpha)
    {
        visibilityTimer = 0;

        Color playerLivesColor = new Color(playerLivesImage.color.r, playerLivesImage.color.g, playerLivesImage.color.b, alpha);
        playerLivesImage.color = playerLivesColor;

        playerLivesText.color = new Color(255,255,255, alpha);

        Color healthColor = new Color(playerHealthbar.color.r, playerHealthbar.color.g, playerHealthbar.color.b, alpha);
        playerHealthbar.color = healthColor;

        Color emptyInvisibleColor = new Color(playerHealthbarBackground.color.r, playerHealthbarBackground.color.g, playerHealthbarBackground.color.b, alpha);
        playerHealthbarBackground.color = emptyInvisibleColor;
    }

    private void DisplayLives()
    {
        StartCoroutine(DisplayLivesRoutine());
    }

    private IEnumerator DisplayLivesRoutine()
    {
        yield return new WaitForSeconds(displayLivesDelay);

        Color playerLivesColor = new Color(playerLivesImage.color.r, playerLivesImage.color.g, playerLivesImage.color.b, 1);
        playerLivesImage.color = playerLivesColor;

        playerLivesText.color = new Color(255, 255, 255, 1);
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnPlayerDisplayUI -= DisplayUI;
        PlayerEventManager.OnPlayerDead -= DisplayLives;
    }

}
