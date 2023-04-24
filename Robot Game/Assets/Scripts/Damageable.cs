using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    protected bool god = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if(!god)
            currentHealth -= damage;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentHealth <= 0)
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
