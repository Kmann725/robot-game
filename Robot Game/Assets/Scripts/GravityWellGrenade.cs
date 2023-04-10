using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellGrenade : Grenade
{
    public override void EmptyHand()
    {
        player.GetComponent<PlayerMovement>().carryingGravity = false;
    }

    public override void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in cols)
        {
            if (col.gameObject.GetComponent<Rigidbody>() && !col.gameObject.CompareTag("Player"))
            {
                Vector3 direction = transform.position - col.gameObject.transform.position;
                col.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * explosionStrength);
            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}