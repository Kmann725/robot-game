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
    private GrenadeMaker gm;
    protected AudioSource src;

    public GameObject explosionEffect;
    public AudioClip soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        carrySpot = GameObject.FindGameObjectWithTag("Carry");
        gm = GameObject.FindGameObjectWithTag("Carry").GetComponent<GrenadeMaker>();
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !thrown)
        {
            carried = false;
            thrown = true;

            if (gameObject.CompareTag("normal grenade"))
            {
                player.GetComponent<PlayerMovement>().carryingNormal = false;
            }
            else if (gameObject.CompareTag("emp grenade"))
            {
                player.GetComponent<PlayerMovement>().carryingEMP = false;
            }
            gm.currentGrenade = null;

            rb.AddForce(mainCamera.transform.forward * throwStrength);
            rb.AddForce(Vector3.up * 450f);
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
            thrown = false;
        }
    }

    public abstract void Explode();
}
