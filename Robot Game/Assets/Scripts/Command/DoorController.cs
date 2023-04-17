using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject redButton;
    public GameObject blueButton;

    private GameObject player;
    private Command openRedDoor;
    private Command openBlueDoor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        openRedDoor = new OpenRedDoor();
        openBlueDoor = new OpenBlueDoor();
    }

    private void Update()
    {
        Vector3 reticlePos = player.transform.position;
        reticlePos.y += 0.61f;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(reticlePos, Camera.main.transform.forward, out var hit, 15f))
            {
                if (hit.collider.gameObject == redButton)
                {
                    openRedDoor.Execute();
                }
                if (hit.collider.gameObject == blueButton)
                {
                    openBlueDoor.Execute();
                }
            }
        }
    }
}
