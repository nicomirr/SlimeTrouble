using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] swordAttackSounds;      
    [SerializeField] private AudioClip[] playerDamagedSounds;      

    private AudioSource audioSource;

    private void Awake()
    {       
        audioSource = GetComponent<AudioSource>();
        PlayerEventManager.OnPlayerAttackPerformed += PlayerAttackSound;
        PlayerEventManager.OnPlayerDamaged += PlayerDamagedSound;
    }

    private void PlayerAttackSound()
    {
        int randomSound = Random.Range(0, swordAttackSounds.Length);
        audioSource.PlayOneShot(swordAttackSounds[randomSound]);        
    }

    private void PlayerDamagedSound()
    {
        int randomSound = Random.Range(0, playerDamagedSounds.Length);
        audioSource.PlayOneShot(playerDamagedSounds[randomSound]);
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnPlayerAttackPerformed -= PlayerAttackSound;
        PlayerEventManager.OnPlayerDamaged -= PlayerDamagedSound;
    }
}
