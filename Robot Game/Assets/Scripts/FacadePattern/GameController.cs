/*
 * Gerard Lamoureux
 * Singleton
 * Team Project 2
 * Overall GameController that uses the Facade Pattern
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    PlayerController playerController;
    ScoreManager scoreManager;

    float inputX;
    float inputZ;

    bool shouldJump;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        scoreManager = ScoreManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        scoreManager.IncrementScore(1);
    }
}
