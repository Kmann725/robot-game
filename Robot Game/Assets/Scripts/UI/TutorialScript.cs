using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TMP_Text text;
    public GameObject TutorialEnemy;
    public int index;
    public string[] sentences;
    public bool intro = true;
    public bool gunBool = false;
    public bool enemyNotDead = true;
    public bool grenadeBool = false;
    public bool buttonBool = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseGame();
        text.text = "Robots have taken over humanity! As a survivor of the apocalypse you are determined save mankind. You have armed yourself with the first weapon you could find and march your way to find the source of the robot threat. \nPress F to continue.";
        TutorialEnemy = GameObject.Find("Tutorial Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (intro == true)
            {
                intro = false;
                resumeGame();
            }


            if (gunBool == false && index < 1)
            {
                TutorialContinue();
            }
            if (grenadeBool == true)
            {
                TutorialContinue();

                if (index > 6)
                {
                    grenadeBool = false;
                    resumeGame();
                    text.enabled = false;
                }
            }
            if (buttonBool == true)
            {
                TutorialContinue();
                if (index == 10)
                {
                    resumeGame();
                    text.enabled = false;
                }
            }


        }
        if (index == 3)
        {
            grenadeBool = true;
        }

        if (index == 1)
        {
            gunBool = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                text.enabled = false;
                gunBool = false;
            }
        }

        if (enemyNotDead == true)
        {
            if (TutorialEnemy.GetComponent<Enemy>().dead == true)
            {
                text.enabled = true;
                enemyNotDead = false;
                TutorialContinue();
            }
        }

    }
    public void GrenadeTutorial()
    {
        TutorialContinue();
        pauseGame();
    }
    public void ButtonTutorial()
    {
        buttonBool = true;
        text.enabled = true;
        TutorialContinue();
        pauseGame();
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
    }
    public void TutorialContinue()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = sentences[index];

        }
    }
}
