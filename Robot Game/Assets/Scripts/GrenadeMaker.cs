using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMaker : MonoBehaviour
{
    public GameObject normalGrenade;

    public void GenerateGrenade()
    {
        Instantiate(normalGrenade, transform.position, transform.rotation);
    }
}
