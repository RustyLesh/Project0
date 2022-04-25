using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    
    [SerializeField] private float maxHealth = 100;

    public delegate void HealthChanged();
    public static event HealthChanged OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Mathf.Clamp((maxHealth -= damage), 0, maxHealth);
        OnHealthChanged.Invoke();
    }

    public void Heal(float healAmount)
    {
        Mathf.Clamp((maxHealth += healAmount), 0, maxHealth);
        OnHealthChanged.Invoke();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float maxHealthValue)
    {
        maxHealth = maxHealthValue;

        if (maxHealth < 1)
            maxHealth = 1;
    }
}
