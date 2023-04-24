using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    string levelName;

    private void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (levelName == "Tutorial")
            {
                SceneManager.LoadScene("Title Menu");
            }
            if(levelName == "Level 1")
            {
                SceneManager.LoadScene("Level 2");
            }
            else if(levelName == "Level 2")
            {
                SceneManager.LoadScene("Level 3");
            }
            else if (levelName == "Level 3")
            {
                
            }
        }
    }
}
