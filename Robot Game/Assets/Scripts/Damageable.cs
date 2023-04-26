using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    protected bool god = false;

    protected AudioSource src;

    public AudioClip hit;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        src = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (!god)
        {
            src.PlayOneShot(hit);
            currentHealth -= damage;
        }
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
