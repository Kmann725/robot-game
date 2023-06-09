using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMaker : MonoBehaviour
{
    public GameObject normalGrenade;
    public GameObject empGrenade;
    public GameObject gravityWellGrenade;

    public GameObject currentGrenade = null;

    private PlayerController pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void GenerateGrenade(int type)
    {
        switch (type)
        {
            case 0:
                if(currentGrenade != null)
                {
                    Destroy(currentGrenade.gameObject);
                    pm.carryingNormal = false;
                    pm.carryingEMP = false;
                    pm.carryingGravity = false;
                }
                break;
            case 1:
                if (currentGrenade == null)
                {
                    currentGrenade = Instantiate(normalGrenade, transform.position, transform.rotation);
                }
                else
                {
                    Destroy(currentGrenade.gameObject);
                    pm.carryingEMP = false;
                    pm.carryingGravity = false;
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
                    pm.carryingGravity = false;
                    currentGrenade = Instantiate(empGrenade, transform.position, transform.rotation);
                }
                break;
            case 3:
                if (currentGrenade == null)
                {
                    currentGrenade = Instantiate(gravityWellGrenade, transform.position, transform.rotation);
                }
                else
                {
                    Destroy(currentGrenade.gameObject);
                    pm.carryingNormal = false;
                    pm.carryingEMP = false;
                    currentGrenade = Instantiate(gravityWellGrenade, transform.position, transform.rotation);
                }
                break;
        }
    }
}
