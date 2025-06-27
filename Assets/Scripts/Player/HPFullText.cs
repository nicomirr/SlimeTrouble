using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFullText : MonoBehaviour
{
    [SerializeField] private float displayTextTime = 1.5f;

    private SpriteRenderer spriteRenderer;

    private bool isDisplaying;


    private void Awake()
    {
        PlayerEventManager.OnDisplayHPFullText += DisplayText;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.enabled = false;
    }

    private void DisplayText()
    {
        if (isDisplaying) return;

        StartCoroutine(DisplayTextRoutine());
    }

    private IEnumerator DisplayTextRoutine()
    {        
        isDisplaying = true;
        spriteRenderer.enabled = true;

        yield return new WaitForSeconds(displayTextTime);

        spriteRenderer.enabled = false;
        isDisplaying = false;
    }


    private void OnDestroy()
    {
        PlayerEventManager.OnDisplayHPFullText -= DisplayText;
    }
}
