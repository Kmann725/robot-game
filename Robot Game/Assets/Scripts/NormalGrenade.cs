using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGrenade : Grenade
{
    public GameObject radiusVisual;

    public override void EmptyHand()
    {
        player.GetComponent<PlayerController>().carryingNormal = false;
        player.GetComponent<PlayerController>().normalGrenadeCount--;
    }

    public override void Explode()
    {
        src.PlayOneShot(soundEffect);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        GameObject newRadius = Instantiate(radiusVisual, transform.position, transform.rotation);

        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in cols)
        {
            if (col.gameObject.GetComponent<Rigidbody>() && !col.gameObject.CompareTag("Player") && !col.isTrigger)
            {
                Vector3 direction = col.gameObject.transform.position - transform.position;
                col.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * explosionStrength);
            }
            if (col.gameObject.CompareTag("Enemy") && !col.isTrigger)
                col.gameObject.GetComponent<Enemy>().TakeDamage(50);
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(newRadius, 0.1f);
        Destroy(gameObject, 0.8f);
    }
}
