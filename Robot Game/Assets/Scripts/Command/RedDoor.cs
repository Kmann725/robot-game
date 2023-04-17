using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
