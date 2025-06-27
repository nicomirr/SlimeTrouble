using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private float recoverTime; 
    private float incomingKnockbackStrength;

    private PlayerMovement playermovement;
    private Rigidbody2D rb;

    public bool KnockingBack { get; private set; }

    private void Awake()
    {
        playermovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector2 direction)
    {
        StartCoroutine(KnockbackRoutine(direction));        
    }

    public void SetIncomingKnockbackStrength(float knockbackStrength)
    {
        incomingKnockbackStrength = knockbackStrength;
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        KnockingBack = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * incomingKnockbackStrength, ForceMode2D.Impulse);

        playermovement.enabled = false;

        yield return new WaitForSeconds(recoverTime);

        playermovement.enabled = true;

        KnockingBack = false;
    }
}

//que el poder del knockback venga de quien ataca en lugar de ser interno al defensor. Por ejemplo, aca habria que sacar knockbackstrength, y hacer que el enemigo provea el poder de knockback