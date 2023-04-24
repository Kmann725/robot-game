using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGreenDoor : MonoBehaviour, Command
{
    GreenDoor greenDoor;

    public OpenGreenDoor()
    {
        greenDoor = GameObject.FindGameObjectWithTag("green door").GetComponent<GreenDoor>();
    }

    public void Execute()
    {
        greenDoor.OpenDoor();
    }
}
