using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private bool isBossBullet;
    public bool IsBossBullet => isBossBullet;

    [SerializeField] private float knockbackStrength = 3f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float lifeTime = 5;

    [SerializeField] private int damage = 4;
    [SerializeField] private float bulletSpeed = 2;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }       

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTimeRoutine());
    }        

    public void LaunchBullet(Vector2 targetDirection)
    {
        rb.velocity = targetDirection * bulletSpeed;
    }

    private IEnumerator BulletLifeTimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth player))
        {
            if(collision.TryGetComponent<PlayerKnockback>(out PlayerKnockback knockback))
                knockback.SetIncomingKnockbackStrength(knockbackStrength);            

            Vector2 damageDirection = (collision.transform.position - this.transform.position).normalized;
            player.TakeDamage(damage, damageDirection);
            this.gameObject.SetActive(false);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer.value) != 0)        
            this.gameObject.SetActive(false);
        
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }    

}
