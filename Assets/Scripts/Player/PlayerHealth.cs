using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 14;
    public int MaxHealth => maxHealth;
    public int CurrentHealth {get ; private set;}

    public Vector2 DamageDirection { get; private set;}
    public bool IsDamaged { get; private set;}      
    
    private Healthbar healthbar;

    private bool isAlive;


    private void Awake()
    {
        PlayerEventManager.OnPlayerHealed += Heal;
        healthbar = GetComponentInChildren<Healthbar>();

        isAlive = true;
    }

    private void Start()
    {
        InitCurrentHealth();        
    }

    private void InitCurrentHealth()
    {
        if (PlayerData.CurrentHealth > 0)
            CurrentHealth = PlayerData.CurrentHealth;
        else
            CurrentHealth = maxHealth;        

        healthbar.UpdateHealthBar(CurrentHealth, maxHealth, 0);
    }

    public void TakeDamage(int damage, Vector2 damageDirection)
    {       
        if(!isAlive) return;

        PlayerEventManager.RaisePlayPlayerDamageSound();
       
        CurrentHealth -= damage;

        healthbar.UpdateHealthBar(CurrentHealth, maxHealth, 1);   //usar evento

        isAlive = CurrentHealth > 0;

        if (isAlive)
        {
            DamageDirection = damageDirection;
            IsDamaged = true;
        }
        else
        {            
            PlayerEventManager.RaisePlayerDead();
        }
    }

    private void Heal(int healingPoints)
    {
        CurrentHealth += healingPoints;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        healthbar.UpdateHealthBar(CurrentHealth, maxHealth, 1);   //usar evento
    }

    public void ResetIsDamaged()
    {
        IsDamaged = false;
    }

    private void OnDestroy()
    {
        PlayerData.SetCurrentHealth(CurrentHealth);
        PlayerEventManager.OnPlayerHealed -= Heal;
    }


}
