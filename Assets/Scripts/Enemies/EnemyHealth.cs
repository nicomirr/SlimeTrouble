using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private AudioClip damagedSound;

    [SerializeField] private int maxHealth;

    public Vector2 DamageDirection { get; private set; }
    public bool IsDamaged { get; private set; }

    private AudioSource audioSource;

    private Healthbar healthbar;
    private BoxCollider2D boxCollider;

    private int currentHealth;
    public int Health => currentHealth;
    
    public bool IsAlive {get; private set;} 

    
    private void Awake()
    {
        IsAlive = true;
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        healthbar = GetComponentInChildren<Healthbar>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int damage, Vector2 damageDirection)
    {
        if (!IsAlive) return;

        audioSource.PlayOneShot(damagedSound);

        if(currentHealth > 0)        
            currentHealth -= damage;

        healthbar.UpdateHealthBar(currentHealth, maxHealth, 1);

        IsAlive = currentHealth > 0;
        if (IsAlive)
        {
            DamageDirection = damageDirection;
            IsDamaged = true;    

        }
        else
        {
            EnemyEventManager.RaiseEnemyDeath();
            boxCollider.enabled = false;
        }
    }
        
    public void ResetIsDamaged()
    {
        IsDamaged = false;
    }    
}
