using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGreenDoor : MonoBehaviour, Command
{
    GreenDoor greenDoor;

    public OpenGreenDoor()
    {
        if (GameObject.FindGameObjectWithTag("green door") != null && GameObject.FindGameObjectWithTag("green door").TryGetComponent(out GreenDoor greenDoor))
            this.greenDoor = greenDoor;
    }

    public void Execute()
    {
        greenDoor.OpenDoor();
    }
}
