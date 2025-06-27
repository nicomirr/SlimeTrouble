using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private int destroyedOrderLayer = 3;

    [SerializeField] private int spawnPotionChance;

    [SerializeField] private GameObject healingPotion;

    [SerializeField] private AudioClip boxHit;
    [SerializeField] private AudioClip boxDestroy;

    [SerializeField] private float health;

    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;

    private readonly int destroyHash = Animator.StringToHash("destroy");

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    private bool isShaking;
    private bool isDestroyed;

    private Vector3 initialPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    public void Shake(Vector2 attackDirection)
    {
        if (isDestroyed) return;
        StartCoroutine(ShakeRoutine(attackDirection));
    }

    private IEnumerator ShakeRoutine(Vector2 attackDirection)
    {
        if (isShaking) yield break;

        health--;
        if (health <= 0)
        {
            audioSource.PlayOneShot(boxDestroy);

            TrySpawnPotion();
            isDestroyed = true;
            boxCollider.enabled = false;
            spriteRenderer.sortingOrder = destroyedOrderLayer;
            animator.SetTrigger(destroyHash);
            this.enabled = false;
            yield break;
        }

        audioSource.PlayOneShot(boxHit);

        isShaking = true;

        this.transform.localPosition = initialPosition + ((Vector3)attackDirection * shakeMagnitude);

        yield return new WaitForSeconds(shakeDuration);

        this.transform.localPosition = initialPosition;

        isShaking = false;
    }

    private void TrySpawnPotion()  
    {
        int trySpawn = Random.Range(1, 101);

        if(trySpawn <= spawnPotionChance)
            Instantiate(healingPotion, this.transform.position, Quaternion.identity);
    }    
}
