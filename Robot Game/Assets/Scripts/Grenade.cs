using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grenade : MonoBehaviour
{
    public float throwStrength = 1000f;
    public float explosionRadius = 5f;
    public float explosionStrength = 500f;
    public bool carried = true;
    public bool thrown = false;

    private Rigidbody rb;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject carrySpot;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        carrySpot = GameObject.FindGameObjectWithTag("Carry");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            carried = false;
            thrown = true;
            player.GetComponent<PlayerMovement>().normalGrenadeCount--;
            player.GetComponent<PlayerMovement>().carryingNormal = false;
            rb.AddForce(mainCamera.transform.forward * throwStrength);
            rb.AddForce(Vector3.up * 300f);
        }

        if (carried)
        {
            transform.position = carrySpot.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown)
        {
            Explode();
        }
    }

    public abstract void Explode();
}
