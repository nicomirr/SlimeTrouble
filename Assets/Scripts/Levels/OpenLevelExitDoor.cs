using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject exitZone;
    [SerializeField] private AudioClip openDoorSound;

    [SerializeField] private float openExitDoorDelay = 2;
    [SerializeField] private GameObject exitDoor;

    private AudioSource audioSource;

    private void Awake()
    {
        LevelEventManager.OnAllEnemiesDestroyed += OpenExitDoor;
        audioSource = GetComponent<AudioSource>();
    }

    private void OpenExitDoor()
    {
        StartCoroutine(OpenExitDoorRoutine());
    }

    private IEnumerator OpenExitDoorRoutine()
    {
        yield return new WaitForSeconds(openExitDoorDelay);
        audioSource.PlayOneShot(openDoorSound);
        exitZone.SetActive(true);
        Destroy(exitDoor);
    }

    private void OnDestroy()
    {
        LevelEventManager.OnAllEnemiesDestroyed -= OpenExitDoor;
    }
}
