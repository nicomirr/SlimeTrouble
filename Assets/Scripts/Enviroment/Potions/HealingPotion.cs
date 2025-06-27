using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    [SerializeField] private int healingPoints = 4;
    public int HealingPoints => healingPoints;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private bool pickedUp;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickedUp) return;

        if(collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (playerHealth.CurrentHealth == playerHealth.MaxHealth)
            {
                PlayerEventManager.RaiseDisplayHPFullText();
                return;
            }

            pickedUp = true;

            audioSource.PlayOneShot(pickupSound);

            PlayerEventManager.RaisePlayerHealed(HealingPoints);
            spriteRenderer.enabled = false;
            
        }
    }

}
