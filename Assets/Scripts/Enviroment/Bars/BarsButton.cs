using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsButton : MonoBehaviour
{
    [SerializeField] private AudioClip buttonPressedSound;

    [SerializeField] private EntranceBars entranceBars;
    [SerializeField] private Bars[] bars;

    private readonly int buttonPressedHash = Animator.StringToHash("buttonPressed");

    private AudioSource audioSource;
    private Animator animator;

    private bool buttonPressed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buttonPressed) return;

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            audioSource.PlayOneShot(buttonPressedSound);

            buttonPressed = true;
            animator.SetTrigger(buttonPressedHash);

            for(int i = 0; i < bars.Length; i++)
            {
                bars[i].BarsDown();
            }

            entranceBars.gameObject.SetActive(true);
            entranceBars.BarsUp();   
        }
    }
}
