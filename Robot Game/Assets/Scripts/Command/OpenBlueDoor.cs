using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBlueDoor : MonoBehaviour, Command
{
    BlueDoor blueDoor;

    public OpenBlueDoor()
    {
        blueDoor = GameObject.FindGameObjectWithTag("blue door").GetComponent<BlueDoor>();
    }

    public void Execute()
    {
        blueDoor.OpenDoor();
    }
}
