using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPGrenade : Grenade
{
    private GameObject newRadius;

    public GameObject empRadius;

    public override void Explode()
    {
        src.PlayOneShot(soundEffect);
        newRadius = Instantiate(empRadius, transform.position, transform.rotation);

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(EnergyWave());
    }

    IEnumerator EnergyWave()
    {
        while (newRadius.transform.localScale.x < 10)
        {
            yield return new WaitForSeconds(0.01f);

            newRadius.transform.localScale += new Vector3(0.18f, 0.18f, 0.18f);
        }

        Destroy(newRadius);
        Destroy(gameObject, 0.5f);
    }
}
