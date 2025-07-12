using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private bool isPlayerHealthbar;

    [SerializeField] private SpriteRenderer healthbar;
    [SerializeField] private SpriteRenderer emptyHealthbar;

    [SerializeField] private float maxWidth = 0.433f;
    [SerializeField] private float visibilityTime = 3f;


    private float visibilityTimer;
    private bool visible;

    
    private void Start()
    {
        ToggleHealthbarVisibility(0);
        healthbar.transform.localScale = new Vector3(maxWidth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);

        visible = false;
    }

    private void Update()
    {
        if (!visible) return;

        VisibilityTimer();

    } 

    private void VisibilityTimer()
    {
        visibilityTimer += Time.deltaTime;
        if (visibilityTimer >= visibilityTime)
        {
            ToggleHealthbarVisibility(0);
            visible = false;
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth, int visibility)
    {
        if(currentHealth <= 0)
            Destroy(this.gameObject);

        visible = true;
        ToggleHealthbarVisibility(visibility);

        float fill = (float)currentHealth / maxHealth;
        
        Vector3 scale = healthbar.transform.localScale;
        scale.x = fill * maxWidth;

        healthbar.transform.localScale = scale;
    }

    private void ToggleHealthbarVisibility(float alpha)
    {
        visibilityTimer = 0;

        Color healthbarColor = new Color(healthbar.color.r, healthbar.color.g, healthbar.color.b, alpha);
        healthbar.color = healthbarColor;

        Color emptyBarColor = new Color(emptyHealthbar.color.r, emptyHealthbar.color.g, emptyHealthbar.color.b, alpha);
        emptyHealthbar.color = emptyBarColor;
    }

}
