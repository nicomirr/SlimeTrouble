using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemiesOnDeath : MonoBehaviour
{
    [SerializeField] private float activationDelay = 0.9f;

    [SerializeField] private Transform[] enemyActivationLocations;
    [SerializeField] private GameObject[] enemies;

    public void EnemyActivationOnDeath()
    {
        StartCoroutine(EnemyActivationOnDeathRotuine());
    }

    private IEnumerator EnemyActivationOnDeathRotuine()
    {
        yield return new WaitForSeconds(activationDelay);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = enemyActivationLocations[i].transform.position;
            enemies[i].SetActive(true);
        }
    }
}
