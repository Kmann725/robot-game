/*
 * Gerard Lamoureux
 * Singleton
 * Team Project 2
 * Overall GameController that uses the Facade Pattern
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    PlayerController playerController;
    ScoreManager scoreManager;
    PauseMenuBehavior pauseMenu;
    WinMenuBehavior winMenu;

    float inputX;
    float inputZ;

    bool shouldJump;

    public GameObject TextBox;

    private TextMeshProUGUI text;

    private bool canDestroyText = false;

    public int level = 0;

    public bool gameLost = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        scoreManager = ScoreManager.Instance;
        pauseMenu = PauseMenuBehavior.Instance;
        winMenu = WinMenuBehavior.Instance;

        TextBox = GameObject.FindGameObjectWithTag("textbox");
        text = TextBox.GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(level > 0)
        {
            if (level == 1)
                text.text = "Welcome to 546 Motor Industries Soldier. Unfortunately, the robots here have gotten out of control. Save Humanity and Disable them all!\n\nMove to Continue.";
            else if (level == 2)
                text.text = "It looks like the elevator took your grenades! I'm sure you can scavenge for more, we NEED to save humanity!\n\nMove to Continue.";
            else if (level == 3)
                text.text = "You're Almost There! The Button to Deactivate the Robots is on this Floor!\n\nMove to Continue.";
            else if (level == 4)
                text.text = "You did it! The Robots are Deactivating! You Saved the World!";
            TextBox.SetActive(true);
            StartCoroutine(InitialText());
        }
    }

    IEnumerator InitialText()
    {
        yield return new WaitForSeconds(3);
        canDestroyText = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameLost && level < 4)
        {
            if (Time.timeScale == 1)
                pauseMenu.gameObject.SetActive(true);
            else if (Time.timeScale == 0)
                pauseMenu.gameObject.SetActive(false);
        }
        if (Time.timeScale == 0)
            return;
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shouldJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.HoldGrenade(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.HoldGrenade(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.HoldGrenade(3);
        }
    }

    private void FixedUpdate()
    {
        if (level > 3) return;
        if ((inputX > 0 || inputZ > 0) && !TutorialScript.IsTutorial && canDestroyText)
            TextBox.SetActive(false);
        playerController.MovePlayer(inputX, inputZ);
        if(shouldJump)
        {
            playerController.JumpPlayer();
            shouldJump = false;
        }
    }

    public void GrabGrenade(GameObject grenade)
    {
        Destroy(grenade);
        playerController.GrabGrenade(grenade.tag);
    }

    public void EnemyKilled()
    {
        if(level < 4)
            scoreManager.IncrementScore(1);
    }

    public void GameLost()
    {
        gameLost = true;
        pauseMenu.gameObject.SetActive(true);
    }

    public void GameWon()
    {
        winMenu.gameObject.SetActive(true);
    }
}
