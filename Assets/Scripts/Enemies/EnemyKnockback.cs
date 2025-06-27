using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackSpeed;

    private EnemyAI enemyAI;

    public bool KnockingBack { get; private set; }  


    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }       

    public void Knockback(Vector2 direction)
    {
        StartCoroutine(KnockbackRoutine(direction));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        KnockingBack = true;
        enemyAI.StopNavigation();

        float timer = 0f;

        while (timer < knockbackDuration)
        {
            this.transform.position += (Vector3)(direction * knockbackSpeed * Time.deltaTime);
            timer += Time.deltaTime;

            yield return null;
        }

        KnockingBack = false;
        enemyAI.ResumeNavigation();
    }
}
