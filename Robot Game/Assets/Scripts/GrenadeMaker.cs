using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMaker : MonoBehaviour
{
    public GameObject normalGrenade;
    public GameObject empGrenade;

    public GameObject currentGrenade = null;

    private PlayerMovement pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void GenerateGrenade(int type)
    {
        switch (type)
        {
            case 1:
                if (currentGrenade == null)
                {
                    currentGrenade = Instantiate(normalGrenade, transform.position, transform.rotation);
                }
                else
                {
                    Destroy(currentGrenade.gameObject);
                    pm.carryingEMP = false;
                    currentGrenade = Instantiate(normalGrenade, transform.position, transform.rotation);
                }
                break;
            case 2:
                if (currentGrenade == null)
                {
                    currentGrenade = Instantiate(empGrenade, transform.position, transform.rotation);
                }
                else
                {
                    Destroy(currentGrenade.gameObject);
                    pm.carryingNormal = false;
                    currentGrenade = Instantiate(empGrenade, transform.position, transform.rotation);
                }
                break;
        }
    }
}
