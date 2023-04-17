using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
