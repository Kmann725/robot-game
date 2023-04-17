using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRedDoor : MonoBehaviour, Command
{
    RedDoor redDoor;

    public OpenRedDoor()
    {
        redDoor = GameObject.FindGameObjectWithTag("red door").GetComponent<RedDoor>();
    }

    public void Execute()
    {
        redDoor.OpenDoor();
    }
}
