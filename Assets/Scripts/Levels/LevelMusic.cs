using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{   
    public static LevelMusic Instance {get; private set;}

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        PlayerEventManager.OnPlayerDead += StopMusic;

        audioSource = GetComponent<AudioSource>();
    }    

    public void ChangeMusic(AudioClip audioClip)
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
    }

    public void StartMusic()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnDestroy()
    {
        PlayerEventManager.OnPlayerDead -= StopMusic;
    }
        
}
