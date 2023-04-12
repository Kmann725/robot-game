using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPWave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.SetState(enemy.shockState);
        }
    }
}
