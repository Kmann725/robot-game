using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePanel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
