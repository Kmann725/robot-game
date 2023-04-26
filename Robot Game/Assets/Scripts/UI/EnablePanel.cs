using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePanelObject(GameObject panelObject)
    {
        panelObject.SetActive(true);
    }

    public void DisablePanelObject(GameObject panelObject)
    {
        panelObject.SetActive(false);
    }
}
