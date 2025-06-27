using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{
    [SerializeField] private float knockbackStrength = 3;

    [SerializeField] private AudioClip spikesUpSound;
    [SerializeField] private AudioClip spikesDownSound;

    [SerializeField] private float cooldownTime = 0.2f;

    [SerializeField] private int damage = 4;

    [SerializeField] private float downDurationTime = 4f;
    [SerializeField] private float upDurationTime = 2;

    private readonly int spikesUpHash = Animator.StringToHash("spikesUp");

    private AudioSource audioSource;
    private BoxCollider2D damageArea;
    private Animator animator;

    private Vector2 damageAreaSize;
    private Vector2 damageAreaOffset;

    private bool spikesUp;
    private bool canDamage = true;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        damageArea = GetComponent<BoxCollider2D>(); 
    }

    private void Start()
    {
        damageAreaSize = damageArea.size;
        damageAreaOffset = damageArea.offset;

        StartCoroutine(InitSpikesRoutine());
    }

    private IEnumerator InitSpikesRoutine()
    {
        yield return new WaitForSeconds(downDurationTime);

        StartCoroutine(SpikesUpRoutine());
    }

    private IEnumerator SpikesDownRoutine()
    {
        audioSource.PlayOneShot(spikesDownSound);

        spikesUp = false;

        animator.SetBool(spikesUpHash, false);
        yield return new WaitForSeconds(downDurationTime);

        StartCoroutine(SpikesUpRoutine());
    }

    private IEnumerator SpikesUpRoutine()
    {
        audioSource.PlayOneShot(spikesUpSound);

        spikesUp = true;

        animator.SetBool(spikesUpHash, true);
        yield return new WaitForSeconds(upDurationTime);

        StartCoroutine(SpikesDownRoutine());
    }

    private void Update()
    {
        DamageHits();       
    }  

    private void DamageHits()
    {
        if (!spikesUp || !canDamage) return;

        Vector2 center = (Vector2)transform.position + damageAreaOffset;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, damageAreaSize, 0f);

        foreach (var hit in hits)
        {
            if (hit != null )
            {
                Vector2 damageDirection = (hit.transform.position - this.transform.position).normalized;

                if (hit.TryGetComponent<PlayerHealth>(out PlayerHealth player))
                {
                    if(hit.TryGetComponent<PlayerKnockback>(out PlayerKnockback knockback))
                        knockback.SetIncomingKnockbackStrength(knockbackStrength);

                    player.TakeDamage(damage, damageDirection);
                }
                else if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
                {
                    enemy.TakeDamage(damage, damageDirection);
                }                
            }
        }

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        canDamage = false;

        yield return new WaitForSeconds(cooldownTime);

        canDamage = true;
    }

}
