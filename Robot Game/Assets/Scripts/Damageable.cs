using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth <= 0 && !gameObject.CompareTag("Player"))
            Destruction();
    }

    protected virtual void Destruction()
    {
        Destroy(gameObject);
    }

    public bool IsDestroyed()
    {
        return currentHealth <= 0;
    }

}
