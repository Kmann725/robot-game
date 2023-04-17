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

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
