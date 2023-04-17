using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            // Damage the player
            other.gameObject.GetComponent<Damageable>().TakeDamage(damage);
        }

        // Destroy the bullet
        if(!other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}