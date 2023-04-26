using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePanelOnStart : MonoBehaviour
{
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }
}
